using EasyCodeForVivox.Events;
using System.Threading.Tasks;
using VivoxUnity;


namespace EasyCodeForVivox
{
    public class EasyUsers : IUsers
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


        public async void OnUserJoinedChannel(object sender, KeyEventArg<string> keyArg)
        {
            var source = (IReadOnlyDictionary<string, IParticipant>)sender;

            var senderIParticipant = source[keyArg.Key];
            EasyEvents.OnUserJoinedChannel(senderIParticipant); 
            if (EasySession.UseDynamicEvents)
            {
                await EasyEventsAsync.OnUserJoinedChannelAsync(senderIParticipant);
            }
        }

        public async void OnUserLeftChannel(object sender, KeyEventArg<string> keyArg)
        {
            var source = (IReadOnlyDictionary<string, IParticipant>)sender;

            var senderIParticipant = source[keyArg.Key];
            EasyEvents.OnUserLeftChannel(senderIParticipant);
            if (EasySession.UseDynamicEvents)
            {
                await EasyEventsAsync.OnUserLeftChannelAsync(senderIParticipant);
            }
            
        }

        public async void OnUserValuesUpdated(object sender, ValueEventArg<string, IParticipant> valueArg)
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
            if (EasySession.UseDynamicEvents)
            {
                await HandleDynamicEvents(valueArg, senderIParticipant);
            }
        }

        private async Task HandleDynamicEvents(ValueEventArg<string, IParticipant> valueArg, IParticipant participant)
        {

            switch (valueArg.PropertyName)
            {
                case "LocalMute":

                    if (!participant.IsSelf) //can't local mute yourself, so don't check for it
                    {
                        if (participant.LocalMute)
                        {
                            // Fires too much
                            await EasyEventsAsync.OnUserMutedAsync(participant);
                        }
                        else
                        {
                            // Fires too much
                            await EasyEventsAsync.OnUserUnmutedAsync(participant);
                        }
                    }
                    break;

                case "SpeechDetected":
                    {
                        if (participant.SpeechDetected)
                        {
                            await EasyEventsAsync.OnUserSpeakingAsync(participant);
                        }
                        else
                        {
                            await EasyEventsAsync.OnUserNotSpeakingAsync(participant);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

    }
}