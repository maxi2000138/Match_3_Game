namespace Popups.LevelComplete
{
    public interface ILevelCompletePopupPresenter
    {
        void OnPlayAgainButtonClick();
        void OnNextLevelButtonClick();
        string PlayAgainText { get; }
        string MainText { get; }
        string NextLevelText { get; }
    }
}