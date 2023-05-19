using EasyCodeForVivox;
using UnityEngine;
using VivoxUnity;
using Zenject;

namespace EasyCodeForVivox.Examples
{
    public class VivoxUsers
    {
        EasyUsers _users;

        [Inject]
        private void Initialize(EasyUsers users)
        {
            _users = users;
        }

        protected virtual void OnUserJoinedChannel(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Has Joined The Channel");
        }

        protected virtual void OnUserLeftChannel(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Has Left The Channel");
        }

        protected virtual void OnUserValuesUpdated(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Has updated itself in the channel");
        }
    }
}
