using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationBlendTreeController : NetworkBehaviour
{
    // Credits : iHeartGameDev :  https://www.youtube.com/watch?v=m8rGyoStfgQ&list=PLwyUzJb_FNeTQwyGujWRLqnfKpV-cj-eO&index=4

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _accelaration = 0.1f;
    [SerializeField] private float _deccelaration = 0.5f;
    [SerializeField][Range(0f, 5f)] private float _maxVelocity = 1f;
    private Animator _animator;
    private float _velocity = 0.0f;
    private int _velocityHash;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _velocityHash = Animator.StringToHash("Velocity");
    }


    // Update is called once per frame
    void Update()
    {
        if (IsOwner && IsSpawned)
        {
            HandleMovement();
            if (_playerMovement.IsGrounded)
            {
                HandleAnimationServerRpc(_velocity);
            }
        }
    }

    public void HandleMovement()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        if (forwardPressed && _velocity < _maxVelocity)
        {
            _velocity += Time.deltaTime * _accelaration;
        }
        if (!forwardPressed && _velocity > 0.0f)
        {
            _velocity -= Time.deltaTime * _deccelaration;
        }
        if (!forwardPressed && _velocity < 0.0f)
        {
            _velocity = 0.0f;
        }
    }

    [ServerRpc]
    private void HandleAnimationServerRpc(float velocity)
    {
        _animator.SetFloat(_velocityHash, velocity);
    }
}
