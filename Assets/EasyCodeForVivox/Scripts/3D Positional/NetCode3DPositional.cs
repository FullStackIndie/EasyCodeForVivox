using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VivoxUnity;
using Zenject;

namespace EasyCodeForVivox
{
    public class NetCode3DPositional : MonoBehaviour
    {
        [Header("3D Positional Settings")]
        public Transform listenerPosition;
        public Transform speakerPosition;
        private Vector3 _lastListenerPosition;
        private Vector3 _lastSpeakerPosition;

        private bool _positionalChannelExists = false;
        private string _channelName;
        private string userName;
        private EasySettingsSO _settings;



        private void Awake()
        {
            userName = EasySession.LoginSessions.FirstOrDefault().Value.LoginSessionId.DisplayName;
            _settings = ScriptableObject.CreateInstance<EasySettingsSO>();
        }

        private void Start()
        {
            StartCoroutine(Handle3DPositionUpdates(.3f, userName));
        }


        IEnumerator Handle3DPositionUpdates(float nextUpdate, string userName)
        {
            yield return new WaitForSeconds(nextUpdate);
            if (EasySession.LoginSessions[userName].State == LoginState.LoggedIn)
            {
                if (_positionalChannelExists)
                {
                    Update3DPosition();
                }
                else
                {
                    _positionalChannelExists = CheckIfChannelExists();
                }
            }

            StartCoroutine(Handle3DPositionUpdates(nextUpdate, userName));
        }

        public bool CheckIfChannelExists()
        {
            foreach (KeyValuePair<string, IChannelSession> session in EasySession.ChannelSessions)
            {
                if (session.Value.Channel.Type == ChannelType.Positional)
                {
                    _channelName = session.Value.Channel.Name;
                    if (EasySession.ChannelSessions[_channelName].ChannelState == ConnectionState.Connected)
                    {
                        if (_settings.LogEasyNetCode)
                            Debug.Log($"Channel : {_channelName} is connected");
                        if (EasySession.ChannelSessions[_channelName].AudioState == ConnectionState.Connected)
                        {
                            if (_settings.LogEasyNetCode)
                                Debug.Log($"Audio is Connected in Channel : {_channelName}");
                            return true;
                        }
                    }
                    else
                    {
                        if (_settings.LogEasyNetCode)
                            Debug.Log($"Channel : {_channelName} is not Connected");
                    }
                }
            }
            return false;
        }


        public void Update3DPosition()
        {
            if (listenerPosition.position != _lastListenerPosition || speakerPosition.position != _lastSpeakerPosition)
            {
                EasySession.ChannelSessions[_channelName].Set3DPosition(speakerPosition.position, listenerPosition.position, listenerPosition.forward, listenerPosition.up);
                if (_settings.LogNetCodeForGameObjects)
                    Debug.Log($"{EasySession.ChannelSessions[_channelName].Channel.Name} 3D positon has been updated");
            }
            _lastListenerPosition = listenerPosition.position;
            _lastSpeakerPosition = speakerPosition.position;
        }
    }
}