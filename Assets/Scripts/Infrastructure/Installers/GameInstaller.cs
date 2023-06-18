using Infrastructure.LoadingCurtain;
using Infrastructure.Services;
using Infrastructure.Services.SceneLoadService;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.CoroutineRunner;
using Infrastructure.StateMachine.States;
using Infrastructure.StateMachine.States.IStates;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCoroutineRunner();
            BindSceneLoadService();
            BindStatesForStateMachine();
            BindGameStateMachine();
            BindLoadingCurtain();
            BindGame();

            Container.Bind<StaticDataService>()
                .AsSingle();
        }

        private void BindGame() => 
            Container.BindInterfacesAndSelfTo<Game>().AsSingle();

        private void BindStatesForStateMachine() =>
            Container.Bind<StateBase>()
                .To(
                    typeof(BootstrapState),
                    typeof(LoadLevelState)
                )
                .WhenInjectedInto<IGameStateMachine>();

        private void BindGameStateMachine() => 
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

        private void BindSceneLoadService() => 
            Container.Bind<ISceneLoadService>().To<SceneLoadService>().AsSingle();
        
        private void BindCoroutineRunner() =>
            Container
                .Bind<ICoroutineRunner>().To<CoroutineRunner>()
                .FromComponentInNewPrefabResource(ResourcePathes.CoroutineRunner).AsSingle();
        
        private void BindLoadingCurtain() =>
            Container
                .Bind<LoadingCurtains>()
                .FromComponentInNewPrefabResource(ResourcePathes.LoadingCurtain).AsSingle();
    }
}
