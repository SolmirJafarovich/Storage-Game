using UnityEngine;
using Zenject;

public interface IInputHandler
{
    void HandleMovement();
    void HandleLook();
}

public class GameInstaller : MonoInstaller
{
    public Transform playerCamera;
    public Transform holdPoint;
    public Transform playerTransform;
    public Joystick joystick;
    public float moveSpeed = 5f;
    public float lookSpeed = 3f;
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
                .WithArguments(playerTransform, playerCamera, moveSpeed, lookSpeed);

        }

        Container.Bind<ItemHandler>().FromComponentInHierarchy().AsSingle();


        Container.BindInstance(holdPoint).WhenInjectedInto<ItemHandler>();


    }
}
