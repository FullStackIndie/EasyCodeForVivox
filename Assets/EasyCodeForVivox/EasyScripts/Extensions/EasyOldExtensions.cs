using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VivoxUnity;

namespace EasyCodeForVivox
{
    [Obsolete("This will be removed in future versions in favor of new EasyExtensions")]
    public static class EasyOldExtensions
    {

        /// <summary>
        /// Checks if this IchannelSession is the current logged in user
        /// </summary>
        /// <param name="channelSession"></param>
        /// <returns></returns>
        public static bool IsSelf(this IChannelSession channelSession)
        {
            if (channelSession.Participants.ContainsKey(GetSIP(channelSession)))
            {
                return true;
            }
            return false;
        }


        #region Text-To-Speech - Extension Methods

        /// <summary>
        /// Play this message locally and override current playing TTS message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginSession"></param>
        public static void TTSMsgLocalPlayOverCurrent(this string message, ILoginSession loginSession)
        {
            TTSMessage msg = new TTSMessage(message, TTSDestination.LocalPlayback);
            loginSession.TTS.Speak(msg);
            // todo add catch for less than 10 msgeages loginSession.TTS.Messages.Count < 10;
        }

        /// <summary>
        /// Play this message remotely and override current playing TTS message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginSession"></param>
        public static void TTS_Msg_Remote_PlayOverCurrent(this string message, ILoginSession loginSession)
        {
            TTSMessage msg = new TTSMessage(message, TTSDestination.RemoteTransmission);
            loginSession.TTS.Speak(msg);
            // todo add catch for less than 10 msgeages loginSession.TTS.Messages.Count < 10;
        }

        /// <summary>
        /// Play this message locally and remotely and ovverride current playing TTS message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginSession"></param>
        public static void TTSMsgLocalRemotePlayOverCurrent(this string message, ILoginSession loginSession)
        {
            TTSMessage msg = new TTSMessage(message, TTSDestination.RemoteTransmissionWithLocalPlayback);
            loginSession.TTS.Speak(msg);
            // todo add catch for less than 10 msgeages loginSession.TTS.Messages.Count < 10;
        }

        /// <summary>
        /// Replace current playing TTS message with this message locally
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginSession"></param>
        public static void TTSMsgLocalReplaceCurrentPlaying(this string message, ILoginSession loginSession)
        {
            TTSMessage msg = new TTSMessage(message, TTSDestination.ScreenReader);
            loginSession.TTS.Speak(msg);
            // todo add catch for less than 10 msgeages loginSession.TTS.Messages.Count < 10;
        }

        /// <summary>
        /// Play TTS message locally, adds to current queue if a message is already playing
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginSession"></param>
        public static void TTSMsgQueueLocal(this string message, ILoginSession loginSession)
        {
            TTSMessage msg = new TTSMessage(message, TTSDestination.QueuedLocalPlayback);
            loginSession.TTS.Messages.Enqueue(msg);
            // todo add catch for less than 10 msgeages loginSession.TTS.Messages.Count < 10;
        }

        /// <summary>
        /// Play TTS message remotely, adds to current queue if a message is already playing
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginSession"></param>
        public static void TTSMsgQueueRemote(this string message, ILoginSession loginSession)
        {
            TTSMessage msg = new TTSMessage(message, TTSDestination.QueuedRemoteTransmission);
            loginSession.TTS.Messages.Enqueue(msg);
            // todo add catch for less than 10 msgeages loginSession.TTS.Messages.Count < 10;
        }

        /// <summary>
        /// Play TTS message remotely and locally, adds to current queue if a message is already playing
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginSession"></param>
        public static void TTSMsgQueueRemoteLocal(this string message, ILoginSession loginSession)
        {
            TTSMessage msg = new TTSMessage(message, TTSDestination.QueuedRemoteTransmissionWithLocalPlayback);
            loginSession.TTS.Messages.Enqueue(msg);
            // todo add catch for less than 10 msgeages loginSession.TTS.Messages.Count < 10;
        }


        #endregion


        #region Get SIP Address - Extension Methods

        /// <summary>
        /// Gets the valid Vivox SIP address from this ILoginSession
        /// </summary>
        /// <param name="loginSession"></param>
        /// <returns></returns>
        public static string GetSIP(this ILoginSession loginSession)
        {
            var user = EasySIP.GetUserSIP(loginSession);
            return user;
        }

        /// <summary>
        /// Gets the valid Vivox SIP address from this IChannelSession
        /// </summary>
        /// <param name="loginSession"></param>
        /// <returns></returns>
        public static string GetSIP(this IChannelSession channelSession)
        {
            var participants = channelSession.Participants;
            var user = EasySIP.GetUserSIP(channelSession.Parent.Key.Issuer, channelSession.Parent.Key.DisplayName, channelSession.Parent.Key.Domain);
            if (participants.ContainsKey(user))
            {
                return user;
            }
            return "Error : Couldnt find user";
        }

        /// <summary>
        /// Gets the valid Vivox SIP address from this IParticipant
        /// </summary>
        /// <param name="loginSession"></param>
        /// <returns></returns>
        public static string GetSIP(this IParticipant participant)
        {
            var SIP = EasySIP.GetUserSIP(participant);
            return SIP;
        }



        #endregion


        #region GameObject - Extension Methods

        /// <summary>
        /// Deactivates this Gameobject and activates another Gameobject
        /// </summary>
        /// <param name="toDeactivate">Gameobject to Deactivate</param>
        /// <param name="toActivate">Gameobject to Activate</param>
        public static void Switch_To(this GameObject toDeactivate, GameObject toActivate)
        {
            toDeactivate.SetActive(false);
            toActivate.SetActive(true);
        }


        #endregion


        #region TMP Dropdown - Extension Methods


        public static void Add_Value(this TMP_Dropdown user_Dropdown_TMP, string valueToAdd)
        {
            user_Dropdown_TMP.options.Add(new TMP_Dropdown.OptionData() { text = valueToAdd });
            user_Dropdown_TMP.RefreshShownValue();
        }

        // Got help from here   Username : Brathnann https://forum.unity.com/threads/how-to-use-dropdown-options-remove.501916/
        public static void Remove_Value(this TMP_Dropdown user_Dropdown_TMP, string valueToRemove)
        {
            TMP_Dropdown.OptionData userToRemove = user_Dropdown_TMP.options.Find((x) => x.text == valueToRemove);
            if (user_Dropdown_TMP.options.Contains(userToRemove))
            {
                user_Dropdown_TMP.options.Remove(userToRemove);
                user_Dropdown_TMP.RefreshShownValue();
            }
        }

        public static void Force_Value(this TMP_Dropdown user_Dropdown_TMP, string valueToFind)
        {
            TMP_Dropdown.OptionData currentUser = user_Dropdown_TMP.options.Find((x) => x.text == valueToFind);
            if (user_Dropdown_TMP.options.Contains(currentUser))
            {
                user_Dropdown_TMP.options[0] = currentUser;
                user_Dropdown_TMP.RefreshShownValue();
            }
        }

        public static string Get_Selected_Value(this TMP_Dropdown user_Dropdown_TMP)
        {
            int index = user_Dropdown_TMP.value;
            string result;
            if (index >= 0 && index < user_Dropdown_TMP.options.Count)
            {
                result = user_Dropdown_TMP.options[index].text;
                return result;
            }
            return null;
        }

        public static string Get_Selected_Value_If_Not(this TMP_Dropdown user_Dropdown_TMP, string toExclude)
        {
            int index = user_Dropdown_TMP.value;
            string result;
            if (index >= 0 && index < user_Dropdown_TMP.options.Count && user_Dropdown_TMP.options[index].text != toExclude)
            {
                result = user_Dropdown_TMP.options[index].text;
                return result;
            }
            return null;
        }

        public static string Get_Selected_Value_If_Not(this TMP_Dropdown user_Dropdown_TMP, string[] toExclude)
        {
            int index = user_Dropdown_TMP.value;
            string result;
            foreach (string exclude in toExclude)
            {
                if (index >= 0 && index < user_Dropdown_TMP.options.Count && user_Dropdown_TMP.options[index].text != exclude)
                {
                    result = user_Dropdown_TMP.options[index].text;
                    return result;
                }
            }
            return null;
        }


        #endregion


        #region Toggle - Extension Methods

        public static void TurnOn(this Toggle toggle)
        {
            toggle.isOn = true;
        }

        public static void TurnOff(this Toggle toggle)
        {
            toggle.isOn = false;
        }

        #endregion

    }
}