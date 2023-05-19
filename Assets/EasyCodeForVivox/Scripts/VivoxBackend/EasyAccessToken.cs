using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace EasyCodeForVivox
{

    /// <summary>
    /// Copied from Vivox General Unity Documentation.
    /// Creates Secure Token for Vivox API requests, needed for production ready applications
    /// <remarks>
    /// 	<para>Slightly Altered From Vivox Example To Create Proper Token</para>
    /// </remarks>
    /// </summary>
    public class EasyAccessToken : MonoBehaviour
    {

        /// <summary>
        /// Vivox Access Token(VAT) format class to generate valid request tokens.
        /// Read more on Vivox Documentation
        /// <remarks>
        /// 	<para>This is the only method needed to create all neccessary types of tokens In Vivox</para>
        /// 	<para>Names and acronyms are mostly consistent with Vivox Documentation to avoid confusion but expanded upon for better understanding</para>
        /// </remarks>
        /// <list type="bullet">
        /// 	<item>
        /// 		<term>key</term>
        /// 		<description> Token Key - Get From Vivox Developer Portal</description>
        /// 	</item>
        /// 	<item>
        /// 		<term>iss</term>
        /// 		<description> Token Issuer - Get From Vivox Developer Portal</description>
        /// 	</item>
        /// 	<item>
        /// 		<term>exp</term>
        /// 		<description> Expiration - Vivox Uses Unix Epoch time - Add Expiration time to Epoch value</description>
        /// 	</item>
        /// 	<item>
        /// 		<term>vxa</term>
        /// 		<description> Vivox Action to perform - Refer To Vivox Documentation</description>
        /// 	</item>
        /// 	<item>
        /// 		<term>vxi</term>
        /// 		<description> Unique Identifier - Create from a custom counter or Unique GUID</description>
        /// 	</item>
        /// 	<item>
        /// 		<term>sub</term>
        /// 		<description> Subject : The User to mute, unmute, kick etc.</description>
        /// 	</item>
        /// 	<item>
        /// 		<term>f</term>
        /// 		<description> From : The User requesting an action, Usually self or Admin.</description>
        /// 	</item>
        /// 	<item>
        /// 		<term>t</term>
        /// 		<description> Channel : The Channel to join, mute, kick, transcribe(Speech-To-Text Vivox Paid Service) etc.</description>
        /// 	</item>
        /// </list>
        /// </summary>
        /// <param name="key">Token Key From Vivox Developer Portal</param>
        /// <param name="issuer">Application Issuer - Vivox Developer Portal</param>
        /// <param name="exp">Time in epoch + 90 seconds or prefered timeout</param>
        /// <param name="vxa">Vivox Action to perform : ex. login, kick, join</param>
        /// <param name="vxi">Unique identifier to garauntee unique Token. Recommended to use counter on server</param>
        /// <param name="sub">sub == Subject : The User to mute, unmute, kick etc.</param>
        /// <param name="f">f == From : The User requesting an action</param>
        /// <param name="t">t == Channel : The Channel to join, mute, kick, transcribe etc.</param>
        /// <remarks>
        /// <para>Token creation for Kicking people from channels, Muting people, Muting All except one person(Presentation Mode).</para>
        /// <para>If (Admin) you can kick people from channels or servers.</para>
        /// <para>If (Admin) you can mute people in channels, muting all except one(Presentation Mode).</para>
        /// <para>SIP URI(Address) required for f, t, and sub.</para>
        /// <para>SUB, F, T Can/Should be Null if not needed for the claim/action request.</para>
        /// <para>ex. login only needs the f paramater, sub == null, t == null.</para>
        /// <para>ex. Token_F("yourTokenKey", "blindmelon-AppName-dev", (int)epochTime, "login", 0001, null, "sip:.blindmelon-AppName-dev.jerky.@tla.vivox.com", null)</para>
        /// </remarks>
        /// <returns>A Valid Token For Production Code with Vivox (JWT with empty header)</returns>
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

        /// <value>
        /// 	Gets Unix Epoch (January 1st, 1970, 00:00:00) to create valid expiration times for Vivox Access Tokens- Used in <see cref="SecondsSinceUnixEpochPlusDuration(TimeSpan?)"></see>
        /// </value>
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        /// <summary>
        /// Copied Implementation From Vivox API.
        /// Used for obtaining time in seconds of Unix Epoch to Now(Current Time) with the option of an added duration.
        /// </summary>
        /// <param name="duration">Timespan ahead of (DateTime.UtcNow - Unix Epoch) you want to have a timestamp for.</param>
        /// <returns>The time in seconds from Unix Epoch (January 1st, 1970, 00:00:00) to DateTime.UtcNow with an added duration.</returns>
        /// see <see cref="EasyAccessToken.CreateToken(string, string, int, string, int, string, string, string)"/> see epoch time.
        public static int SecondsSinceUnixEpochPlusDuration(TimeSpan? duration = null)
        {
            TimeSpan timestamp = DateTime.UtcNow.Subtract(UnixEpoch);
            if (duration.HasValue)
            {
                timestamp = timestamp.Add(duration.Value);
            }

            return (int)timestamp.TotalSeconds;
        }

        /// <summary>
        /// Creates Base64 Encoded string from the Json payload and then strips the padding from it <see href="https://www.rfc-editor.org/rfc/rfc7515#appendix-C"></see>
        /// </summary>
        private static string Base64URLEncode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            // Remove padding at the end
            var encodedString = Convert.ToBase64String(plainTextBytes).TrimEnd('=');
            // Substitute URL-safe characters
            string urlEncoded = encodedString.Replace("+", "-").Replace("/", "_");

            return urlEncoded;
        }

        /// <summary>
        /// Signs the JWT payload and hashes the data using HMAC and SHA256 algorithms
        /// </summary>
        /// <param name="secret">The secret.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
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



    /// <summary>
    /// Vivox Access Token(VAT) format class to generate valid request tokens.
    /// Read more on Vivox Documentation
    /// <remarks>
    /// 	<para>
    /// 	Class/Model that will be seriliazed by <see cref="UnityEngine.JsonUtility">JsonUtility</see> 
    /// 	to create the JSON payload that will be used to create the Vivox Access Token
    /// 	</para>
    /// </remarks>
    /// <list type="bullet">
    /// 	<item>
    /// 		<term>iss</term>
    /// 		<description> Token Issuer - Get From Vivox Developer Portal</description>
    /// 	</item>
    /// 	<item>
    /// 		<term>exp</term>
    /// 		<description> Expiration - Vivox Uses Unix Epoch time - Add Expiration time to Epoch value</description>
    /// 	</item>
    /// 	<item>
    /// 		<term>vxa</term>
    /// 		<description> Vivox Action to perform - Login, Join Channel, Kick, Mute etc.</description>
    /// 	</item>
    /// 	<item>
    /// 		<term>vxi</term>
    /// 		<description> Unique Identifier - Create from a custom counter or Unique GUID</description>
    /// 	</item>
    /// 	<item>
    /// 		<term>sub</term>
    /// 		<description> Subject : The User to mute, unmute, kick etc.</description>
    /// 	</item>
    /// 	<item>
    /// 		<term>f</term>
    /// 		<description> From : The User requesting an action, Usually self or Admin.</description>
    /// 	</item>
    /// 	<item>
    /// 		<term>t</term>
    /// 		<description> Channel : The Channel to join, mute, kick, transcribe(Speech-To-Text Vivox Paid Service) etc.</description>
    /// 	</item>
    /// </list>
    /// </summary>
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

