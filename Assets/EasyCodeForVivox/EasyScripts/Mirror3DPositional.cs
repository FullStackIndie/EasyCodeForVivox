using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;

#if MIRROR_3_12_OR_NEWER
using Mirror;
#endif

namespace EasyCodeForVivox
{

#if MIRROR_3_12_OR_NEWER

    public class Mirror3DPositional : NetworkBehaviour
    {
        [Header("3D Positional Settings")]
        public Transform listenerPosition;
        public Transform speakerPosition;
        private Vector3 _lastListenerPosition;
        private Vector3 _lastSpeakerPosition;

        private bool _positionalChannelExists = false;
        private string _channelName;


        public override void OnStartServer()
        {
            Debug.Log("Player has been spawned to the server!");
        }

        private void Start()
        {
            StartCoroutine(Handle3DPositionUpdates(.3f));
        }

        IEnumerator Handle3DPositionUpdates(float nextUpdate)
        {
            if (isLocalPlayer) // todo destroy if not mine to save resources
            {
                yield return new WaitForSeconds(nextUpdate);
                if (EasySession.mainLoginSession.State == LoginState.LoggedIn)
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
            }

            StartCoroutine(Handle3DPositionUpdates(nextUpdate));
        }


        public bool CheckIfChannelExists()
        {
            foreach (KeyValuePair<string, IChannelSession> session in EasySession.mainChannelSessions)
            {
                if (session.Value.Channel.Type == ChannelType.Positional)
                {
                    _channelName = session.Value.Channel.Name;
                    if (EasySession.mainChannelSessions[_channelName].ChannelState == ConnectionState.Connected)
                    {
                        Debug.Log($"Channel : {_channelName} is connected");
                        if (EasySession.mainChannelSessions[_channelName].AudioState == ConnectionState.Connected)
                        {
                            Debug.Log($"Audio is Connected in Channel : {_channelName}");
                            return true;
                        }
                    }
                    else
                    {
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
                EasySession.mainChannelSessions[_channelName].Set3DPosition(speakerPosition.position, listenerPosition.position, listenerPosition.forward, listenerPosition.up);
                Debug.Log($"{EasySession.mainChannelSessions[_channelName].Channel.Name} 3D positon has been updated");
            }
            _lastListenerPosition = listenerPosition.position;
            _lastSpeakerPosition = speakerPosition.position;
        }

    }

#endif

}