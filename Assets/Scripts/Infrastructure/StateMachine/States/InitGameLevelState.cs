using Infrastructure.Installers;
using Infrastructure.LoadingCurtain;
using Infrastructure.Services;
using Infrastructure.StateMachine.States.IStates;

namespace Infrastructure.StateMachine.States
{
    public class InitGameLevelState : StateBase
    {
        private readonly LoadingCurtains _loadingCurtain;
        private readonly TilesInitializer _tilesInitializer;
        private readonly TilesSpawner _tilesSpawner;
        private readonly TileClickEventer _tileClickEventer;
        private readonly LevelDataLoader _levelDataLoader;

        public InitGameLevelState(LoadingCurtains loadingCurtain, TilesInitializer tilesInitializer
            , TilesSpawner tilesSpawner, TileClickEventer tileClickEventer
            , LevelDataLoader levelDataLoader)
        {
            _loadingCurtain = loadingCurtain;
            _tilesInitializer = tilesInitializer;
            _tilesSpawner = tilesSpawner;
            _tileClickEventer = tileClickEventer;
            _levelDataLoader = levelDataLoader;
        }

        public override void OnEnter()
        {
            _levelDataLoader.LoadLevel();

            _tilesSpawner.Spawn();
            _tilesSpawner.InitTileInitializer();
            _tilesSpawner.InitTileClickEventer();

            _tilesInitializer.InitializeTiles();
            _tileClickEventer.InitializeEvents();

            _loadingCurtain.Fade();
            
            EnterGameState();
        }
        
        private void EnterGameState() => 
            Game.EnterState<GameLoopState>();
    }
}