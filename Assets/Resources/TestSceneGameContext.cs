using UnityEngine;
using Zenject;

public class TestSceneGameContext : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}