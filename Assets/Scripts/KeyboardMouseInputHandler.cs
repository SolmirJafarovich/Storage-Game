using UnityEngine;

public class KeyboardMouseInputHandler : IInputHandler
{
    private readonly Transform _playerTransform; 
    private readonly Transform _cameraTransform; 
    private readonly float _moveSpeed;
    private readonly float _lookSpeed;

    private float _xRotation;

    public KeyboardMouseInputHandler(Transform playerTransform, Transform cameraTransform, float moveSpeed, float lookSpeed)
    {
        _playerTransform = playerTransform;
        _cameraTransform = cameraTransform;
        _moveSpeed = moveSpeed;
        _lookSpeed = lookSpeed;
    }

    public void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = _playerTransform.right * moveX + _playerTransform.forward * moveZ;
        _playerTransform.position += move * _moveSpeed * Time.deltaTime;
    }

    public void HandleLook()
    {
        float viewX = Input.GetAxis("Mouse X");
        float viewY = Input.GetAxis("Mouse Y");

        _xRotation -= viewY * _lookSpeed;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _playerTransform.Rotate(Vector3.up * viewX);
    }

    public bool IsPickupPressed()
    {
        return Input.GetMouseButtonDown(0); 
    }

    public bool IsDropPressed()
    {
        return Input.GetMouseButtonDown(1); 
    }

    public bool IsThrowPressed()
    {
        return Input.GetKeyDown(KeyCode.Space); 
    }
}
