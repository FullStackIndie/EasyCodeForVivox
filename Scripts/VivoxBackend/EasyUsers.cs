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
            await _eventsAsync.OnUserValuesUpdatedAsync(senderIParticipant);

            switch (valueArg.PropertyName)
            {
                case "LocalMute":

                    if (!senderIParticipant.IsSelf) //can't local mute yourself, so don't check for it
                    {
                        if (senderIParticipant.LocalMute)
                        {
                            // Fires too much
                            _events.OnUserMuted(senderIParticipant);
                            await _eventsAsync.OnUserMutedAsync(senderIParticipant);
                        }
                        else
                        {
                            // Fires too much
                            _events.OnUserUnmuted(senderIParticipant);
                            await _eventsAsync.OnUserUnmutedAsync(senderIParticipant);
                        }
                    }
                    break;

                case "SpeechDetected":
                    {
                        if (senderIParticipant.SpeechDetected)
                        {
                            _events.OnUserSpeaking(senderIParticipant);
                            await _eventsAsync.OnUserSpeakingAsync(senderIParticipant);
                        }
                        else
                        {
                            _events.OnUserNotSpeaking(senderIParticipant);
                            await _eventsAsync.OnUserNotSpeakingAsync(senderIParticipant);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

    }
}