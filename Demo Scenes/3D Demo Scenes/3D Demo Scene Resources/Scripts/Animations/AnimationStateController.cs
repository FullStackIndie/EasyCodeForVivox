using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class AnimationStateController : NetworkBehaviour
{
    // Credit :  iHeartGameDev  : https://www.youtube.com/watch?v=FF6kezDQZ7s&list=PLwyUzJb_FNeTQwyGujWRLqnfKpV-cj-eO&index=3

    [SerializeField] private PlayerMovement _playerMovement;
    Animator _animator;
    int _isWalkingHash;
    int _isRunningHash;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _isWalkingHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner && IsSpawned)
        {
            HandleMovement();
        }
    }

    public void HandleMovement()
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        if (_playerMovement.IsGrounded)
        {
            if (!isWalking && Input.GetKey(KeyCode.W))
            {
                HandleWalkAnimationServerRpc(true);
            }
            if (isWalking && !forwardPressed)
            {
                HandleWalkAnimationServerRpc(false);
            }
        }
    }

    [ServerRpc]
    private void HandleWalkAnimationServerRpc(bool walk)
    {
        _animator.SetBool(_isWalkingHash, walk);
    }
}
