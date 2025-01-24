using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private IItemHolder _itemHandlerPC;
    private IInputHandler _inputHandler;

    [Inject]
    public void Construct(IItemHolder itemHandlerPC, IInputHandler inputHandler)
    {
        _itemHandlerPC = itemHandlerPC;
        _inputHandler = inputHandler;
    }

    private void Update()
    {
        _inputHandler.HandleLook();
        _inputHandler.HandleMovement();

        HandleItemInteractions();
    }

    private void HandleItemInteractions()
    {
        if (_inputHandler.IsPickupPressed())
        {
            _itemHandlerPC.TryPickupObject();
        }

        if (_inputHandler.IsDropPressed())
        {
            _itemHandlerPC.DropObject();
        }

        if (_inputHandler.IsThrowPressed())
        {
            _itemHandlerPC.ThrowObject();
        }
    }
}
