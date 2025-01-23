using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public Transform playerCamera;
    public Transform holdPoint;

    public override void InstallBindings()
    {
        // Подключение компонентов
        Container.Bind<PlayerMovement>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ItemHandler>().FromComponentInHierarchy().AsSingle();

        // Внедрение дополнительных параметров
        Container.BindInstance(playerCamera).WhenInjectedInto<PlayerMovement>();
        Container.BindInstance(holdPoint).WhenInjectedInto<ItemHandler>();
    }
}
