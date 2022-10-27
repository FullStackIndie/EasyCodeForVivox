using EasyCodeForVivox.Events;
using System.Threading.Tasks;
using VivoxUnity;


namespace EasyCodeForVivox
{
    public class EasyUsers : IUsers
    {
        private readonly EasyEvents _events;
        private readonly EasyEventsAsync _eventsAsync;

        public EasyUsers(EasyEvents events, EasyEventsAsync eventsAsync)
        {
            _events = events;
            _eventsAsync = eventsAsync;
        }

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
            _events.OnUserJoinedChannel(senderIParticipant);
            await _eventsAsync.OnUserJoinedChannelAsync(senderIParticipant);
        }

        public async void OnUserLeftChannel(object sender, KeyEventArg<string> keyArg)
        {
            var source = (IReadOnlyDictionary<string, IParticipant>)sender;

            var senderIParticipant = source[keyArg.Key];
            _events.OnUserLeftChannel(senderIParticipant);
            await _eventsAsync.OnUserLeftChannelAsync(senderIParticipant);

        }

        public async void OnUserValuesUpdated(object sender, ValueEventArg<string, IParticipant> valueArg)
        {
            var source = (IReadOnlyDictionary<string, IParticipant>)sender;

            var senderIParticipant = source[valueArg.Key];
            _events.OnUserValuesUpdated(senderIParticipant);

            switch (valueArg.PropertyName)
            {
                case "LocalMute":

                    if (!senderIParticipant.IsSelf) //can't local mute yourself, so don't check for it
                    {
                        if (senderIParticipant.LocalMute)
                        {
                            // Fires too much
                            _events.OnUserMuted(senderIParticipant);
                        }
                        else
                        {
                            // Fires too much
                            _events.OnUserUnmuted(senderIParticipant);
                        }
                    }
                    break;

                case "SpeechDetected":
                    {
                        if (senderIParticipant.SpeechDetected)
                        {
                            _events.OnUserSpeaking(senderIParticipant);
                        }
                        else
                        {
                            _events.OnUserNotSpeaking(senderIParticipant);
                        }
                        break;
                    }
                default:
                    break;
            }
            await HandleDynamicEvents(valueArg, senderIParticipant);
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
                            await _eventsAsync.OnUserMutedAsync(participant);
                        }
                        else
                        {
                            // Fires too much
                            await _eventsAsync.OnUserUnmutedAsync(participant);
                        }
                    }
                    break;

                case "SpeechDetected":
                    {
                        if (participant.SpeechDetected)
                        {
                            await _eventsAsync.OnUserSpeakingAsync(participant);
                        }
                        else
                        {
                            await _eventsAsync.OnUserNotSpeakingAsync(participant);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

    }
}