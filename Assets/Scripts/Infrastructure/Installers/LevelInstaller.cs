using Infrastructure.StateMachine.States;
using Infrastructure.StateMachine.States.IStates;
using Zenject;

namespace Infrastructure.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            AddLevelStates();
        }

        private void AddLevelStates()
        {
            Container.Bind<StateBase>()
                .To(
                    typeof(InitGameLevelState),
                    typeof(GameLoopState)
                )
                .WhenInjectedInto<LevelStateInjector>();
            Container.BindInterfacesTo<LevelStateInjector>().AsSingle();
        }
    }
}