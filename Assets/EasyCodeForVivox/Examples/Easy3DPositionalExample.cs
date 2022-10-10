using System.Collections;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class Easy3DPositionalExample : MonoBehaviour
    {
        [Header("3D Positional Settings")]
        // Gameobject/Character - An empty gameobject that you attach to a Human model or a cube gameobject if you want.
        // the listener is basically the ears so place wherever the characters face/ears, should be facing the same way as the model
        public Transform listenerPosition;
        // Gameobject/Character - An empty gameobject that you attach to a Human model or a cube gameobject if you want.
        // the speaker is basically the mouth so place wherever the characters mouth should be,  should be facing the same way as the model
        public Transform speakerPosition;
        // records the last position of the Listener
        private Vector3 _lastListenerPosition;
        // records the last position of the Speaker
        private Vector3 _lastSpeakerPosition;

        // a local variable to capture existing Channel Session
        private IChannelSession _channelSession;
        // a local variable to capture existing Login Session
        private ILoginSession _loginSession;

        // a local variable to capture state of an existing ChannelSession
        private bool _positionalChannelExists = false;
        // a local variable to capture the name of existing ChannelSession
        private string _channelName;


        private void Awake()
        {
            // get an existing instance of a IChanelSession from whatever script you are using to  Join channel
            _channelSession = GetComponent<YourScriptWithCurrentChannelAndLoginSession>().channelSession;
            // get an existing instance of a IChanelSession from whatever script you are using to Login into Vivox 
            _loginSession = GetComponent<YourScriptWithCurrentChannelAndLoginSession>().loginSession;
        }

        private void Start()
        {
            // Starts a Courotine that waits until the Character/Gameobject has moved
            // using a Courotine instead of the Update method for better performance instead of checking every frame
            // and also checking every .3 seconds - You can adjust this value to whatever you like
            StartCoroutine(Handle3DPositionUpdates(.3f));
        }

        IEnumerator Handle3DPositionUpdates(float nextUpdate)
        {
            // Waits for the time wanted until the code below(if statement) is executed
            yield return new WaitForSeconds(nextUpdate);
            // checks if user is logged in to prevent any errors
            if (_loginSession.State == LoginState.LoggedIn)
            {
                // checks if positinonal channel exists to prevent errors
                if (_positionalChannelExists)
                {
                    // if positional channel exists handle Updating Logic
                    Update3DPosition();
                }
                else
                {
                    // checks if positinonal channel exists to prevent errors
                    _positionalChannelExists = CheckIfChannelExists();
                }
            }
            // Restarts the Courotine that was initially started in the Start method
            StartCoroutine(Handle3DPositionUpdates(nextUpdate));
        }

        public bool CheckIfChannelExists()
        {
            // checks if the channel is Positional to prevent any errors
            if (_channelSession.Channel.Type == ChannelType.Positional)
            {
                // gets/saves channel name to a variable
                _channelName = _channelSession.Channel.Name;
                // checks if channel is connected to prevent errors
                if (_channelSession.ChannelState == ConnectionState.Connected)
                {
                    Debug.Log($"Channel : {_channelName} is connected");
                    // checks if the Audio channel is connected to prevent errors
                    if (_channelSession.AudioState == ConnectionState.Connected)
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
            return false;
        }


        public void Update3DPosition()
        {
            // if the Listener or Speaker gameobject are in a different position than the last time we checked
            if (listenerPosition.position != _lastListenerPosition || speakerPosition.position != _lastSpeakerPosition)
            {
                // we send this Info to Vivox so they can update the 3D positional channel with the new values
                // If anyone is close to your player depending on the direction they are facing, they should be able to hear them.
                // This is based on the Channel3DProperties() you provide when join a 3D psotional channel
                _channelSession.Set3DPosition(speakerPosition.position, listenerPosition.position, listenerPosition.forward, listenerPosition.up);
                Debug.Log($"{_channelSession.Channel.Name} 3D positon has been updated");
            }
            // Updates the Listener or Speaker gameobject positions for the next time we check
            _lastListenerPosition = listenerPosition.position;
            _lastSpeakerPosition = speakerPosition.position;
        }
    }
}