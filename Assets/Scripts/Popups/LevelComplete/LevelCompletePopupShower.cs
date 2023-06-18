namespace Popups.LevelComplete
{
    public class LevelCompletePopupShower
    {
        private LevelCompletePopupPresenter.Factory _presenterFactory;
        private IPopup _popup;

        public LevelCompletePopupShower(LevelCompletePopupPresenter.Factory presenterFactory, LevelCompletePopup popup)
        {
            _popup = popup;
            _presenterFactory = presenterFactory;
        }

        public void Show()
        {
            ILevelCompletePopupPresenter levelCompletePopupPresenter = _presenterFactory.Create();
            _popup.Show(levelCompletePopupPresenter);
        }
    }
}
