using UnityEngine;

public class KeyboardMouseInputHandler : IInputHandler
{
    private readonly Transform _playerTransform; // ������ �� Transform ���������
    private readonly Transform _cameraTransform; // ������ �� Transform ������
    private readonly Transform _holdPoint;
    private readonly float _moveSpeed;
    private readonly float _lookSpeed;

    private GameObject _heldObject; // ������� �������� ������
    private float _xRotation;

    public KeyboardMouseInputHandler(Transform playerTransform, Transform cameraTransform, Transform holdPoint, float moveSpeed, float lookSpeed)
    {
        _playerTransform = playerTransform;
        _cameraTransform = cameraTransform;
        _holdPoint = holdPoint;
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

/*    public void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0)) // ����� ������ ����
        {
            if (_heldObject == null)
            {
                // ��������� ������ ����� �������
                if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, 3f))
                {
                    var interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactable.Interact();
                        _heldObject = hit.collider.gameObject;

                        // ���������� ������ � ����� ���������
                        _heldObject.transform.SetParent(_holdPoint);
                        _heldObject.transform.localPosition = Vector3.zero;
                        _heldObject.transform.localRotation = Quaternion.identity;
                        _heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
            }
            else
            {
                // ���� ������ ��� ������������, ��������� ���
                _heldObject.transform.SetParent(null);
                _heldObject.GetComponent<Rigidbody>().isKinematic = false;
                _heldObject = null;
            }
        }
    }*/
}
