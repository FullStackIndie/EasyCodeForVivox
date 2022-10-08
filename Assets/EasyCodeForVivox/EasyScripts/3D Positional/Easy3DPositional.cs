using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{

#if MIRROR_3_12_OR_NEWER
using Mirror;
#endif

    using UnityEngine.SceneManagement;
    using VivoxUnity;

#if MIRROR_3_12_OR_NEWER

public class Mirror_3DPositionalAudio : NetworkBehaviour
{
    [Header("Player Settings")]
    public Transform listenerPos;
    public Transform speakerPos;
    public float nextPositionUpdate;
    public List<Vector3> listenerPositions;
    public List<Vector3> speakerPositions;


    void HandleMovement()
    {
        if (isLocalPlayer) // todo destroy if not mine to save resources
        {
            float moveVertical = Input.GetAxis("Vertical"); // if using old input system/default input system
            float moveHorizontal = Input.GetAxis("Horizontal"); // if using old input system/default input system
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
            transform.position += movement; // move character
        }
    }



   // todo figure out to handle channels

    private void Start()
    {
        nextPositionUpdate = Time.time;
    }  


    private void Update()
    {
     

        HandleMovement();

        // replace Vivox_StaticManager with whatever script that has your current IChannelSession

        //if (Time.time > nextPositionUpdate)
        //{
        //    if (VivoxBehaviour.mainChannelSessions[].AudioState == ConnectionState.Connected)
        //    {
        //        if (Vivox_StaticManager.mainChannelSession_3D.Key.Name == Vivox_StaticManager.channel3D_Name && Vivox_StaticManager.mainChannelSession_3D.ChannelState == ConnectionState.Connected)
        //        {
        //            Update3D_Position();
        //        }
        //    }
        //    nextPositionUpdate += 0.3f;
        //}



    }


    public override void OnStartServer()
    {
        Debug.Log("Player has been spawned to the server!");
    }

    public void Update3D_Position()
    {

        // replace Vivox_StaticManager with whatever script that has your current IChannelSession

        //if (!SceneManager.GetActiveScene().buildIndex.Equals(1))
        //{
        //    return;
        //}

        //if (!listenerPositions.Contains(listenerPos.position) && !speakerPositions.Contains(speakerPos.position))
        //{
        //    listenerPositions.Add(listenerPos.position);
        //    speakerPositions.Add(speakerPos.position);
        //    Vivox_StaticManager.mainChannelSession_3D.Set3DPosition(speakerPos.position, listenerPos.position, listenerPos.forward, listenerPos.up);
        //    Debug.Log($"{Vivox_StaticManager.mainChannelSession_3D.Channel.Name} position is being updated");
        //}

    }



}

#endif


    public class Easy3DPositional : MonoBehaviour
    {
        [Header("3D Positional Settings")]
        public Transform listenerPosition;
        public Transform speakerPosition;
        private Vector3 _lastListenerPosition;
        private Vector3 _lastSpeakerPosition;

        private bool _positionalChannelExists = false;
        private string _channelName;


        private void Start()
        {
            StartCoroutine(Handle3DPositionUpdates(.3f));
        }

        IEnumerator Handle3DPositionUpdates(float nextUpdate)
        {
            yield return new WaitForSeconds(nextUpdate);
            if (EasySession.MainLoginSession.State == LoginState.LoggedIn)
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

            StartCoroutine(Handle3DPositionUpdates(nextUpdate));
        }

        public bool CheckIfChannelExists()
        {
            foreach (KeyValuePair<string, IChannelSession> session in EasySession.MainChannelSessions)
            {
                if (session.Value.Channel.Type == ChannelType.Positional)
                {
                    _channelName = session.Value.Channel.Name;
                    if (EasySession.MainChannelSessions[_channelName].ChannelState == ConnectionState.Connected)
                    {
                        Debug.Log($"Channel : {_channelName} is connected");
                        if (EasySession.MainChannelSessions[_channelName].AudioState == ConnectionState.Connected)
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
                EasySession.MainChannelSessions[_channelName].Set3DPosition(speakerPosition.position, listenerPosition.position, listenerPosition.forward, listenerPosition.up);
                Debug.Log($"{EasySession.MainChannelSessions[_channelName].Channel.Name} 3D positon has been updated");
            }
            _lastListenerPosition = listenerPosition.position;
            _lastSpeakerPosition = speakerPosition.position;
        }
    }
}