using Infrastructure.LoadingCurtain;
using Infrastructure.Services;
using Infrastructure.StateMachine.States.IStates;

namespace Infrastructure.StateMachine.States
{
    public class BootstrapState : StateBase
    {
        private readonly LoadingCurtains _loadingCurtains;
        private readonly StaticDataService _staticDataService;

        public BootstrapState(LoadingCurtains loadingCurtains, StaticDataService staticDataService)
        {
            _loadingCurtains = loadingCurtains;
            _staticDataService = staticDataService;
        }
        
        public override void OnEnter()
        {
            _loadingCurtains.Show();
            _staticDataService.Load();
            Game.EnterState<LoadLevelState>();
        }
    }
}