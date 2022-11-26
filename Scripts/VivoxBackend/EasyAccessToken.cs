using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace EasyCodeForVivox
{
    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AccessToken.xml' path='EasyCode/AccessToken[@name="EasyAccessToken"]'/>
    public class EasyAccessToken : MonoBehaviour
    {
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AccessToken.xml' path='EasyCode/AccessToken[@name="CreateToken"]'/>
        public static string CreateToken(string key, string issuer, int exp, string vxa, int vxi, string sub, string f, string t)
        {
            var claims = new Claims
            {
                iss = issuer,
                exp = exp,
                vxa = vxa,
                vxi = vxi,
                f = f,
                t = t,
                sub = sub,
            };

            List<string> segments = new List<string>();
            // Header is static - base64url encoded {} - Can also be defined as a constant "e30"
            var header = Base64URLEncode("{}");
            segments.Add(header);

            // Encode payload
            var claimsString = JsonUtility.ToJson(claims);
            var encodedClaims = Base64URLEncode(claimsString);

            // Join segments to prepare for signing
            segments.Add(encodedClaims);
            string toSign = String.Join(".", segments);

            // Sign token with key and SHA256
            string sig = SHA256Hash(key, toSign);
            segments.Add(sig);

            // Join all 3 parts of token with . and return
            string token = String.Join(".", segments);

            return token;
        }

        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AccessToken.xml' path='EasyCode/AccessToken[@name="UnixEpoch"]'/>
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AccessToken.xml' path='EasyCode/AccessToken[@name="SecondsSinceUnixEpochPlusDuration"]'/>
        public static int SecondsSinceUnixEpochPlusDuration(TimeSpan? duration = null)
        {
            TimeSpan timestamp = DateTime.UtcNow.Subtract(UnixEpoch);
            if (duration.HasValue)
            {
                timestamp = timestamp.Add(duration.Value);
            }

            return (int)timestamp.TotalSeconds;
        }

        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AccessToken.xml' path='EasyCode/AccessToken[@name="Base64URLEncode"]'/>
        private static string Base64URLEncode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            // Remove padding at the end
            var encodedString = Convert.ToBase64String(plainTextBytes).TrimEnd('=');
            // Substitute URL-safe characters
            string urlEncoded = encodedString.Replace("+", "-").Replace("/", "_");

            return urlEncoded;
        }

        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AccessToken.xml' path='EasyCode/AccessToken[@name="SHA256Hash"]'/>
        private static string SHA256Hash(string secret, string message)
        {
            var encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                var hashString = Convert.ToBase64String(hashmessage).TrimEnd('=');
                string urlEncoded = hashString.Replace("+", "-").Replace("/", "_");

                return urlEncoded;
            }
        }
    }


    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AccessToken.xml' path='EasyCode/AccessToken[@name="Claims"]'/>
    public class Claims
    {
        /// <summary>
        /// Issuer : Get from Vivox Developer Portal
        /// </summary>
        public string iss { get; set; }
        /// <summary>
        /// Epoch Time : Vivox uses Unix Epoch time.
        /// ex. DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        /// </summary>
        public int exp { get; set; }
        /// <summary>
        /// VixoxAction : ex. login, join, mute
        /// </summary>
        public string vxa { get; set; }
        /// <summary>
        /// Token Uniqueness Identifier : Can be any number.
        /// Recommended to use an incrimental counter so every token generated will always be different.
        /// ex. int counter = 0;
        /// counter++;
        /// </summary>
        public int vxi { get; set; }
        /// <summary>
        /// Subject : The user to be muted, kicked, unmuted
        /// ex. format == 	sip:.blindmelon-AppName-dev.jerky.@tla.vivox.com
        /// </summary>
        public string sub { get; set; }
        /// <summary>
        /// From : The user requesting a claim/action
        /// ex. format == 	sip:.blindmelon-AppName-dev.beef.@tla.vivox.com
        /// </summary>
        public string f { get; set; }
        /// <summary>
        /// Channel : Channel where action/claim takes place.
        /// ex. format ==	sip:confctl-g-blindmelon-AppName-dev.testchannel@tla.vivox.com
        /// </summary>
        public string t { get; set; }

    }

}

