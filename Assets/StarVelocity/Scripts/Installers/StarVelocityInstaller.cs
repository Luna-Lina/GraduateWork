using StarVelocity.Controllers;
using StarVelocity.Data;
using UnityEngine;
using Zenject;

public class StarVelocityInstaller : MonoInstaller
{
    [SerializeField] private GameObject _firebaseWrapperPrefab;
    [SerializeField] private MainThreadDispatcher _mainThreadDispatcherPrefab;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AdManager _adManager;

    public override void InstallBindings()
    {
        var fb = Container.InstantiatePrefab(_firebaseWrapperPrefab);
        Container.Bind<FirebaseWrapper>().FromInstance(fb.GetComponent<FirebaseWrapper>()).AsSingle();
        Container.Bind<MainThreadDispatcher>().FromInstance(_mainThreadDispatcherPrefab).AsSingle();
        Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();
        Container.Bind<AdManager>().FromInstance(_adManager).AsSingle();
    }
}