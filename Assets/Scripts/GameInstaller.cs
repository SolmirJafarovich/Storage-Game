using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public Transform playerCamera;
    public Transform holdPoint;

    public override void InstallBindings()
    {
        // ����������� �����������
        Container.Bind<PlayerMovement>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ItemHandler>().FromComponentInHierarchy().AsSingle();

        // ��������� �������������� ����������
        Container.BindInstance(playerCamera).WhenInjectedInto<PlayerMovement>();
        Container.BindInstance(holdPoint).WhenInjectedInto<ItemHandler>();
    }
}
