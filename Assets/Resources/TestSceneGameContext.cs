using UnityEngine;
using Zenject;

public class TestSceneGameContext : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle().NonLazy();
        SignalBusInstaller.Install(Container);
        SignalBindings();
    }

    private void SignalBindings()
    {
        //Container.DeclareSignal<>();
    }
}