using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSpeed = 3f;
    private Transform _cameraTransform;
    public Joystick joystick;

    public bool joystickactive = false;

    public float rotationSpeed = 5f; // Скорость поворота персонажа

    private Vector2 _startTouchPosition;
    private Vector2 _currentTouchPosition;
    private bool _isSwiping = false;

    // Внедрение через конструктор
    [Inject]
    public void Construct(Transform cameraTransform)
    {
        _cameraTransform = cameraTransform;
    }

    private float xRotation = 0f;

    public void HandleMovement()
    {
        float moveX;
        float moveZ;
        if (joystickactive)
        {
            moveX = joystick.Horizontal;
            moveZ = joystick.Vertical;
        }
        else
        {
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
        }

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    public void HandleLook()
    {
        if (joystickactive && joystick.Horizontal == 0 && joystick.Vertical == 0)
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
                            float horizontalRotation = delta.x * rotationSpeed * Time.deltaTime;
                            transform.Rotate(0, horizontalRotation, 0);

                            // Поворачиваем персонажа по вертикали (оси X)
                            xRotation -= delta.y * rotationSpeed * Time.deltaTime;
                            xRotation = Mathf.Clamp(xRotation, -80f, 80f); // Ограничиваем вертикальный угол поворота

                            Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

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
        else if (!joystickactive)
        {
            // Управление мышью для десктопа
            float viewX = Input.GetAxis("Mouse X") * lookSpeed;
            float viewY = Input.GetAxis("Mouse Y") * lookSpeed;
            xRotation -= viewY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            _cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * viewX);
        }
    }
}
