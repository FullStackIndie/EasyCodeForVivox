﻿using VivoxUnity;


namespace EasyCodeForVivox
{
    public class EasyUsers
    {

        public void SubscribeToParticipantEvents(IChannelSession channelSession)
        {
            channelSession.Participants.AfterKeyAdded += OnUserJoinedChannel;
            channelSession.Participants.BeforeKeyRemoved += OnUserLeftChannel;
            channelSession.Participants.AfterValueUpdated += OnUserValuesUpdated;
        }

        public void UnsubscribeFromParticipantEvents(IChannelSession channelSession)
        {
            channelSession.Participants.AfterKeyAdded -= OnUserJoinedChannel;
            channelSession.Participants.BeforeKeyRemoved -= OnUserLeftChannel;
            channelSession.Participants.AfterValueUpdated -= OnUserValuesUpdated;
        }


        private void OnUserJoinedChannel(object sender, KeyEventArg<string> keyArg)
        {
            var source = (IReadOnlyDictionary<string, IParticipant>)sender;

            var senderIParticipant = source[keyArg.Key];
            EasyEvents.OnUserJoinedChannel(senderIParticipant);
        }

        private void OnUserLeftChannel(object sender, KeyEventArg<string> keyArg)
        {
            var source = (IReadOnlyDictionary<string, IParticipant>)sender;

            var senderIParticipant = source[keyArg.Key];
            EasyEvents.OnUserLeftChannel(senderIParticipant);
        }

        private void OnUserValuesUpdated(object sender, ValueEventArg<string, IParticipant> valueArg)
        {
            var source = (IReadOnlyDictionary<string, IParticipant>)sender;

            var senderIParticipant = source[valueArg.Key];
            EasyEvents.OnUserValuesUpdated(senderIParticipant);

            switch (valueArg.PropertyName)
            {
                case "LocalMute":

                    if (!senderIParticipant.IsSelf) //can't local mute yourself, so don't check for it
                    {
                        if (senderIParticipant.LocalMute)
                        {
                            // Fires too much
                            EasyEvents.OnUserMuted(senderIParticipant);
                        }
                        else
                        {
                            // Fires too much
                            EasyEvents.OnUserUnmuted(senderIParticipant);
                        }
                    }
                    break;

                case "SpeechDetected":
                    {
                        if (senderIParticipant.SpeechDetected)
                        {
                            EasyEvents.OnUserSpeaking(senderIParticipant);
                        }
                        else
                        {
                            EasyEvents.OnUserNotSpeaking(senderIParticipant);
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }
}