using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private ItemHandler _itemHandler;

    [Inject]
    public void Construct(PlayerMovement playerMovement, ItemHandler itemHandler)
    {
        _playerMovement = playerMovement;
        _itemHandler = itemHandler;
    }

    private void Update()
    {
        _playerMovement.HandleLook();
        _playerMovement.HandleMovement();

        if (Input.GetKeyDown(KeyCode.E)) // Поднять или бросить объект
        {
            if (_itemHandler.HeldObject == null)
                _itemHandler.TryPickupObject(_playerMovement.transform);
            else
                _itemHandler.DropObject();
        }

        if (Input.GetMouseButtonDown(0) && _itemHandler.HeldObject != null) // Бросить объект
        {
            _itemHandler.ThrowObject(_playerMovement.transform);
        }
    }
}
