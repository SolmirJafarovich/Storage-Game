using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private ItemHandler _itemHandler;
    private IInputHandler _inputHandler;

    [Inject]
    public void Construct(ItemHandler itemHandler, IInputHandler inputHandler)
    {
        _itemHandler = itemHandler;
        _inputHandler = inputHandler;
    }

    private void Update()
    {
        _inputHandler.HandleLook();
        _inputHandler.HandleMovement();
    }
/*        if (Input.GetKeyDown(KeyCode.E)) // Поднять или бросить объект
        {
            if (_itemHandler.HeldObject == null)
                _itemHandler.TryPickupObject(_inputHandler.transform);
            else
                _itemHandler.DropObject();
        }

        if (Input.GetMouseButtonDown(0) && _itemHandler.HeldObject != null) // Бросить объект
        {
            _itemHandler.ThrowObject(_inputHandler.transform);
        }
    }*/
}
