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

        if (Input.GetMouseButtonDown(0)) // Клавиша для подбора
        {
            _itemHandlerPC.TryPickupObject();
        }
    }
}
