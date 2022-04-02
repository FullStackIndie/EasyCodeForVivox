using System;
using UnityEngine;
using VivoxUnity;


namespace EasyCodeForVivox
{
    public class EasyUsers
    {
        public static event Action<IParticipant> UserJoinedChannel;
        public static event Action<IParticipant> UserLeftChannel;
        public static event Action<IParticipant> UserValuesUpdated;

        public static event Action<IParticipant> UserMuted;
        public static event Action<IParticipant> UserUnmuted;

        public static event Action<IParticipant> UserSpeaking;
        public static event Action<IParticipant> UserNotSpeaking;


        public void SubscribeToParticipants(IChannelSession channelSession)
        {
            channelSession.Participants.AfterKeyAdded += OnUserJoinedChannel;
            channelSession.Participants.BeforeKeyRemoved += OnUserLeftChannel;
            channelSession.Participants.AfterValueUpdated += OnUserValuesUpdated;
        }

        public void UnsubscribeFromParticipants(IChannelSession channelSession)
        {
            channelSession.Participants.AfterKeyAdded -= OnUserJoinedChannel;
            channelSession.Participants.BeforeKeyRemoved -= OnUserLeftChannel;
            channelSession.Participants.AfterValueUpdated -= OnUserValuesUpdated;
        }



        #region Participant/User Events


        private void OnUserJoinedChannel(IParticipant participant)
        {
            if (participant != null)
            {
                UserJoinedChannel?.Invoke(participant);
            }
        }

        private void OnUserLeftChannel(IParticipant participant)
        {
            if (participant != null)
            {
                UserLeftChannel?.Invoke(participant);
            }
        }

        private void OnUserValuesUpdated(IParticipant participant)
        {
            if (participant != null)
            {
                UserValuesUpdated?.Invoke(participant);
            }
        }



        private void OnUserMuted(IParticipant participant)
        {
            if (participant != null)
            {
                UserMuted?.Invoke(participant);
            }
        }

        private void OnUserUnmuted(IParticipant participant)
        {
            if (participant != null)
            {
                UserUnmuted?.Invoke(participant);
            }
        }

        private void OnUserSpeaking(IParticipant participant)
        {
            if (participant != null)
            {
                UserSpeaking?.Invoke(participant);
            }
        }

        private void OnUserNotSpeaking(IParticipant participant)
        {
            if (participant != null)
            {
                UserNotSpeaking?.Invoke(participant);
            }

        }


        #endregion



        private void OnUserJoinedChannel(object sender, KeyEventArg<string> keyArg)
        {
            var source = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;

            var senderIParticipant = source[keyArg.Key];
            OnUserJoinedChannel(senderIParticipant);
        }

        private void OnUserLeftChannel(object sender, KeyEventArg<string> keyArg)
        {
            var source = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;

            var senderIParticipant = source[keyArg.Key];
            OnUserLeftChannel(senderIParticipant);
        }

        private void OnUserValuesUpdated(object sender, ValueEventArg<string, IParticipant> valueArg)
        {
            var source = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;

            var senderIParticipant = source[valueArg.Key];
            OnUserValuesUpdated(senderIParticipant);

            switch (valueArg.PropertyName)
            {
                case "LocalMute":

                    if (!senderIParticipant.IsSelf) //can't local mute yourself, so don't check for it
                    {
                        if (senderIParticipant.LocalMute)
                        {
                            // Fires too much
                            OnUserMuted(senderIParticipant);
                        }
                        else
                        {
                            // Fires too much
                            OnUserUnmuted(senderIParticipant);
                        }
                    }
                    break;

                case "SpeechDetected":
                    {
                        if (senderIParticipant.SpeechDetected)
                        {
                            OnUserSpeaking(senderIParticipant);
                        }
                        else
                        {
                            OnUserNotSpeaking(senderIParticipant);
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }
}