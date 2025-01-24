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
        if (Input.touchCount > 0)
        {
            // Проверяем активность джойстика
            bool isJoystickActive = Mathf.Abs(_joystick.Horizontal) > 0.1f || Mathf.Abs(_joystick.Vertical) > 0.1f;

            // Если джойстик активен, обрабатываем второе касание
            Touch touch = (Input.touchCount > 1 && isJoystickActive) ? Input.GetTouch(1) : Input.GetTouch(0);

            // Если джойстик активен и нет второго касания, выходим из метода
            if (isJoystickActive && Input.touchCount == 1)
            {
                return;
            }

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

  
                            float horizontalRotation = delta.x * _lookSpeed * Time.deltaTime;
                            _playerTransform.Rotate(0, horizontalRotation, 0);


                            _xRotation -= delta.y * _lookSpeed * Time.deltaTime;
                            _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

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
    
    public bool IsPickupPressed()
    {

        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

    private float lastTapTime = 0f; 
    private const float doubleTapThreshold = 0.3f; 

    public bool IsDropPressed()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                if (Time.time - lastTapTime <= doubleTapThreshold)
                {
                    Debug.Log("Сброс активирован (двойной тап)");
                    lastTapTime = 0f;
                    return true;
                }
                lastTapTime = Time.time;
            }
        }

        return false;
    }


    public bool IsThrowPressed()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved && touch.deltaPosition.y > 50)
            {
                return true;
            }
        }

        return false;
    }
}
