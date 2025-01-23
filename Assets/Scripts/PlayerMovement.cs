using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSpeed = 3f;
    private Transform _cameraTransform;
    public Joystick joystick;

    public bool joystickactive = true;

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
        else {
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
        }


        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    public void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        _cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
