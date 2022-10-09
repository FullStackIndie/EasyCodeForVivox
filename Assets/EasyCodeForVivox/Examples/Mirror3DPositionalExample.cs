
#if MIRROR_3_12_OR_NEWER
using Mirror;
#endif

using UnityEngine.SceneManagement;
using VivoxUnity;

#if MIRROR_3_12_OR_NEWER

public class Mirror3DPositionalExample : NetworkBehaviour
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