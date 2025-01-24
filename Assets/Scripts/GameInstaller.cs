using UnityEngine;
using Zenject;

public interface IInputHandler
{
    void HandleMovement();
    void HandleLook();
    bool IsPickupPressed(); 
    bool IsDropPressed();   
    bool IsThrowPressed();
}

public interface IItemHolder
{
    void TryPickupObject();
    void PickupObject(GameObject obj); 
    void DropObject();   
    void ThrowObject(); 
}


public class GameInstaller : MonoInstaller
{
    public Transform playerCamera;
    public Transform holdPoint;
    public Transform playerTransform;
    public Joystick joystick;
    public float moveSpeed = 5f;
    public float lookSpeed = 3f;
    public float pickupRange = 5f;
    public float throwForce = 15f;
    public bool useTouchControls;

    public override void InstallBindings()
    {

        // Определяем тип ввода
        if (useTouchControls)
        {
            Container.Bind<IInputHandler>()
                .To<MobileInputHandler>()
                .AsSingle()
                .WithArguments(playerTransform, playerCamera, joystick, moveSpeed, lookSpeed);
        }
        else
        {
            Container.Bind<IInputHandler>()
                .To<KeyboardMouseInputHandler>()
                .AsSingle()
                .WithArguments(playerTransform, playerCamera, holdPoint, moveSpeed, lookSpeed);
        }

        Container.Bind<IItemHolder>()
            .To<ItemHolderPC>()
            .AsSingle()
            .WithArguments(playerCamera, holdPoint, pickupRange, throwForce);
    }
}
