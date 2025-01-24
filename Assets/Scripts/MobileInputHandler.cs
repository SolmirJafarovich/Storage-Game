using UnityEngine;

public class MobileInputHandler : IInputHandler
{

    private readonly Transform _playerTransform; // Ссылка на Transform персонажа
    private readonly Transform _cameraTransform; // Ссылка на Transform камеры
    private readonly Joystick _joystick;
    private readonly float _moveSpeed;
    private readonly float _lookSpeed;
    private float _xRotation;

    private Vector2 _startTouchPosition;
    private Vector2 _currentTouchPosition;
    private bool _isSwiping = false;

    public MobileInputHandler(Transform playerTransform, Transform cameraTransform, Joystick joystick, float moveSpeed, float lookSpeed)
    {
        _playerTransform = playerTransform;
        _cameraTransform = cameraTransform;
        _joystick = joystick;
        _moveSpeed = moveSpeed;
        _lookSpeed = lookSpeed;
    }

    public void HandleMovement()
    {
        float moveX = _joystick.Horizontal;
        float moveZ = _joystick.Vertical;

        Vector3 move = _playerTransform.right * moveX + _playerTransform.forward * moveZ;
        _playerTransform.position += move * _moveSpeed * Time.deltaTime;
    }

    public void HandleLook()
    {
        if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
        {
            // Управление камерой только если джойстик не используется
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _startTouchPosition = touch.position;
                        _isSwiping = true;
                        break;

                    case TouchPhase.Moved:
                        if (_isSwiping)
                        {
                            _currentTouchPosition = touch.position;
                            Vector2 delta = _currentTouchPosition - _startTouchPosition;

                            // Поворачиваем персонажа по горизонтали (оси Y)
                            float horizontalRotation = delta.x * _lookSpeed * Time.deltaTime;
                            _playerTransform.Rotate(0, horizontalRotation, 0);

                            // Поворачиваем персонажа по вертикали (оси X)
                            _xRotation -= delta.y * _lookSpeed * Time.deltaTime;
                            _xRotation = Mathf.Clamp(_xRotation, -80f, 80f); // Ограничиваем вертикальный угол поворота

                            Camera.main.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

                            _startTouchPosition = _currentTouchPosition;
                        }
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        _isSwiping = false;
                        break;
                }
            }
        }
    }
}
