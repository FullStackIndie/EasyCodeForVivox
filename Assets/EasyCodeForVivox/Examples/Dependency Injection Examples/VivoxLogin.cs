using EasyCodeForVivox;
using UnityEngine;
using VivoxUnity;
using Zenject;

namespace EasyCodeForVivox.Examples
{
    public class VivoxLogin : MonoBehaviour
    {
        private EasyLogin _login;

        [Inject]
        private void Initialize(EasyLogin login)
        {
            _login = login;
        }

        public void Login()
        {
            _login.LoginToVivox("username");
        }

        public void LoginAsMuted()
        {
            _login.LoginToVivox("username", true);
        }

        public void Logout()
        {
            _login.LogoutOfVivox("username");
        }

        public void UpdateLoginProperties()
        {
            _login.UpdateLoginProperties("username", VivoxUnity.ParticipantPropertyUpdateFrequency.StateChange);
        }


        public void SetPlayerTransmissionModeAll()
        {
            _login.SetPlayerTransmissionMode("username", TransmissionMode.All);
        }

        public void SetPlayerTransmissionModeSingle()
        {
            var channelId = _login.GetChannelId("username", "channelName");
            _login.SetPlayerTransmissionMode("username", TransmissionMode.Single, channelId);
        }


    }
}