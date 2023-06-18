using Infrastructure.Services;
using Infrastructure.StateMachine.States;
using Infrastructure.StateMachine.States.IStates;
using Zenject;

namespace Popups.LevelFailed
{
    public class LevelFailedPopupPresenter : ILevelFailedPopupPresenter
    {
        private readonly Game _game;
        private LevelDataLoader _levelDataLoader;

        public string MainText =>
            "Level Failed!";

        public string PlayAgainText =>
            "Again";


        public LevelFailedPopupPresenter(Game game, LevelDataLoader levelDataLoader)
        {
            _levelDataLoader = levelDataLoader;
            _game = game;
        }
    
        public void OnPlayAgainButtonClick()
        {
            _levelDataLoader.LoadPreviousLevel();
            _game.EnterState<LoadLevelState>();
        }

        public class Factory : PlaceholderFactory<ILevelFailedPopupPresenter>
        {
        }
    }
}