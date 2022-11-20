using EasyCodeForVivox.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class Easy3DPositional : MonoBehaviour
    {
        [Header("3D Positional Settings")]
        public Transform listenerPosition;
        public Transform speakerPosition;
        private Vector3 _lastListenerPosition;
        private Vector3 _lastSpeakerPosition;

        private bool _positionalChannelExists = false;
        private string _channelName;
        private string userName;


        private void Awake()
        {
            userName = EasySession.LoginSessions.FirstOrDefault().Value.LoginSessionId.DisplayName;
        }

        private void Start()
        {
            if (string.IsNullOrEmpty(userName))
            {
                Debug.Log("Could not find User Login Session. Could not enable 3d Postional Updates".Color(EasyDebug.Yellow));
            }
            else
            {
                StartCoroutine(Handle3DPositionUpdates(.3f, userName));
            }
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

        public bool CheckIfChannelExists([CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            foreach (KeyValuePair<string, IChannelSession> session in EasySession.ChannelSessions)
            {
                if (session.Value.Channel.Type == ChannelType.Positional)
                {
                    _channelName = session.Value.Channel.Name;
                    if (EasySession.ChannelSessions[_channelName].ChannelState == ConnectionState.Connected)
                    {
                        Debug.Log($"3D Positional Channel : {_channelName} is connected");
                        if (EasySession.ChannelSessions[_channelName].AudioState == ConnectionState.Connected)
                        {
                            Debug.Log($"Audio is Connected in Channel : {_channelName}");
                            return true;
                        }
                    }
                    else
                    {
                        Debug.Log($"3D Positional Channel : {_channelName} is not Connected");
                    }
                }
                else
                {
                    Debug.Log($"Did not find an active 3D Positional Channel : Cannot activate 3D Positional Voice. \n".Color(EasyDebug.Yellow) +
                        $"Stopping Coroutine at {memberName.Color(EasyDebug.Red)} at line {lineNumber.ToString().Color(EasyDebug.Red)} in {filePath.Color(EasyDebug.Red)}");
                    StopCoroutine(nameof(Handle3DPositionUpdates));
                }
            }
            return false;
        }


        public void Update3DPosition()
        {
            if (listenerPosition.position != _lastListenerPosition || speakerPosition.position != _lastSpeakerPosition)
            {
                EasySession.ChannelSessions[_channelName].Set3DPosition(speakerPosition.position, listenerPosition.position, listenerPosition.forward, listenerPosition.up);
                Debug.Log($"3D positon for {userName} has been updated in channel {EasySession.ChannelSessions[_channelName].Channel.Name}".Color(EasyDebug.Green));
            }
            _lastListenerPosition = listenerPosition.position;
            _lastSpeakerPosition = speakerPosition.position;
        }
    }
}