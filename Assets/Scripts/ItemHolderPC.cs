using UnityEngine;

public class ItemHolderPC : IItemHolder
{
    private readonly Transform _cameraTransform; // Ссылка на Transform камеры
    private readonly Transform _holdPoint;
    private readonly float _pickupRange;
    private readonly float _throwForce;

    private GameObject _heldObject; // Текущий поднятый объект

    public ItemHolderPC(Transform cameraTransform, Transform holdPoint, float pickupRange, float throwForce)
    {
        _cameraTransform = cameraTransform;
        _holdPoint = holdPoint;
        _pickupRange = pickupRange;
        _throwForce = throwForce;
    }

    public void TryPickupObject()
    {
        Ray ray = _cameraTransform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, _pickupRange))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                PickupObject(hit.collider.gameObject);
            }
        }
    }

    public void PickupObject(GameObject obj)
    {
        _heldObject = obj;
        _heldObject.GetComponent<Rigidbody>().isKinematic = true;
        _heldObject.transform.position = _holdPoint.position;
        _heldObject.transform.parent = _holdPoint;
    }

    public void DropObject()
    {
        if (_heldObject != null)
        {
            _heldObject.GetComponent<Rigidbody>().isKinematic = false;
            _heldObject.transform.parent = null;
            _heldObject = null;
        }
    }

    public void ThrowObject()
    {
        if (_heldObject != null)
        {
        Rigidbody rb = _heldObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(_cameraTransform.forward * _throwForce, ForceMode.Impulse);
        _heldObject.transform.parent = null;
        _heldObject = null;
        }
    }
}