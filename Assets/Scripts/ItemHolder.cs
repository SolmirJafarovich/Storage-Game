using UnityEngine;
using Zenject;

public class ItemHandler : MonoBehaviour
{
    private Transform _holdPoint;  // Поле для точки удержания

    [Inject]
    public void Construct(Transform holdPoint)
    {
        _holdPoint = holdPoint;  // Внедрение точки удержания через конструктор
    }
    [SerializeField] private float pickupRange = 5f;
    [SerializeField] private float throwForce = 10f;

    private Rigidbody heldObject;

    public Rigidbody HeldObject => heldObject;

    public void TryPickupObject(Transform cameraTransform)
    {
        if (heldObject != null) return;

        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                var objectRb = hit.collider.GetComponent<Rigidbody>();
                if (objectRb != null)
                {
                    heldObject = objectRb;
                    heldObject.useGravity = false;
                    heldObject.velocity = Vector3.zero;
                    heldObject.angularVelocity = Vector3.zero;
                }
            }
        }
    }

    public void DropObject()
    {
        if (heldObject != null)
        {
            heldObject.useGravity = true;
            heldObject = null;
        }
    }

    public void ThrowObject(Transform cameraTransform)
    {
        if (heldObject != null)
        {
            heldObject.useGravity = true;
            heldObject.AddForce(cameraTransform.forward * throwForce, ForceMode.Impulse);
            heldObject = null;
        }
    }

    private void FixedUpdate()
    {
        if (heldObject != null)
        {
            Vector3 targetPosition = _holdPoint.position;
            Vector3 moveDirection = (targetPosition - heldObject.position);
            heldObject.velocity = moveDirection * 10f;
        }
    }
}
