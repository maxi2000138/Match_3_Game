using Infrastructure.LoadingCurtain;
using Infrastructure.StateMachine.States.IStates;

namespace Infrastructure.Installers
{
    public class GameLoopState : StateBase
    {
        private readonly LoadingCurtains _loadingCurtain;

        public GameLoopState(LoadingCurtains loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
        }
        
        public override void Exit()
        {
            _loadingCurtain.Show();
        }
    }
}