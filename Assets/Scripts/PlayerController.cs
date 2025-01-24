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
        // Попытка подобрать объект
        if (_inputHandler.IsPickupPressed())
        {
            _itemHandlerPC.TryPickupObject();
        }

        // Сброс объекта
        if (_inputHandler.IsDropPressed())
        {
            _itemHandlerPC.DropObject();
        }

        // Бросок объекта
        if (_inputHandler.IsThrowPressed())
        {
            _itemHandlerPC.ThrowObject();
        }
    }
}
