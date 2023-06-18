using Infrastructure.Services;
using Infrastructure.StateMachine.States;
using Infrastructure.StateMachine.States.IStates;
using Zenject;

namespace Popups.LevelComplete
{
    public class LevelCompletePopupPresenter : ILevelCompletePopupPresenter
    {
        private readonly Game _game;
        private LevelDataLoader _levelDataLoader;

        public string MainText =>
            "Level Complete!";

        public string PlayAgainText =>
            "Again";

        public string NextLevelText =>
            "Next";
    
    
        public LevelCompletePopupPresenter(Game game, LevelDataLoader levelDataLoader)
        {
            _levelDataLoader = levelDataLoader;
            _game = game;
        }

        public void OnNextLevelButtonClick()
        {
            _levelDataLoader.SetRandomAnotherLevel();
            _levelDataLoader.SaveCurrentLevel();
            _game.EnterState<LoadLevelState>();
        }

        public void OnPlayAgainButtonClick()
        {
            _levelDataLoader.LoadPreviousLevel();
            _game.EnterState<LoadLevelState>();
        }
    
        public class Factory : PlaceholderFactory<ILevelCompletePopupPresenter>
        {
        }
    }
}