using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MouseLook : NetworkBehaviour
{
    [SerializeField] private float _mouseSensitivity = 100f;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private Camera _camera;
    private float xRotation = 0f;

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            _camera.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        if (!Application.isFocused)
        {
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        if (Cursor.lockState == CursorLockMode.None)
        {
            return;
        }
        if (IsOwner && IsSpawned)
        {
            var mouseX = HandleMouseLook();
            HandlePlayerRotationServerRpc(mouseX);
        }
    }

    private float HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        return mouseX;
    }

    [ServerRpc]
    private void HandlePlayerRotationServerRpc(float mouseX)
    {
        _playerBody.Rotate(Vector3.up * mouseX);
    }

}
