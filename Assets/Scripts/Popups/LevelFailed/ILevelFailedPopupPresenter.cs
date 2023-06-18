namespace Popups.LevelFailed
{
    public interface ILevelFailedPopupPresenter
    {
        string MainText { get; }
        string PlayAgainText { get; }
        void OnPlayAgainButtonClick();
    }
}