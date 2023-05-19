﻿using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public bool IsGrounded { get; private set; }

    [SerializeField] private float _moveSpeed = 12f;
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _gravity = -19.81f;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;

    private CharacterController _characterController;
    private Vector3 _velocity;
    private Vector3 _lastPosition;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _lastPosition = transform.position;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner && IsSpawned)
        {
            IsGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
            if (IsGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 inputDirection = transform.right * horizontal + transform.forward * vertical;

            MovePlayerServerRpc(inputDirection * _moveSpeed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
                HandleJumpAnimationServerRpc(_velocity * Time.deltaTime);
                return;
            }
            if (_lastPosition == transform.position) { return; }
            _velocity.y += _gravity * Time.deltaTime;
            MovePlayerServerRpc(_velocity * Time.deltaTime);
        }
    }


    [ServerRpc]
    private void MovePlayerServerRpc(Vector3 moveDirection)
    {
        _characterController.Move(moveDirection);
    }

    [ServerRpc]
    private void HandleJumpAnimationServerRpc(Vector3 velocity)
    {
        _characterController.Move(velocity);
    }

}