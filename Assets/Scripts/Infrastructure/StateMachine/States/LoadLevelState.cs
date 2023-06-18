using Infrastructure.Configs;
using Infrastructure.Services.SceneLoadService;
using Infrastructure.StateMachine.States.IStates;

namespace Infrastructure.StateMachine.States
{
    public class LoadLevelState : StateBase
    {
        private readonly ISceneLoadService _loadService;
        private readonly ScenesConfig _scenesConfig;

        public LoadLevelState(ISceneLoadService loadService, ScenesConfig scenesConfig)
        {
            _scenesConfig = scenesConfig;
            _loadService = loadService;
        }
        
        public override void OnEnter()
        {
            _loadService.LoadScene(_scenesConfig.GameSceneSettings.Name, EnterInitGameLevelState);
        }

        private void EnterInitGameLevelState() => 
            Game.EnterState<InitGameLevelState>();
    }
}