namespace Popups.LevelFailed
{
    public class LevelFailedPopupShower
    {
        private LevelFailedPopupPresenter.Factory _presenterFactory;
        private IPopup _popup;

        public LevelFailedPopupShower(LevelFailedPopupPresenter.Factory presenterFactory, LevelFailedPopup popup)
        {
            _popup = popup;
            _presenterFactory = presenterFactory;
        }

        public void Show()
        {
            ILevelFailedPopupPresenter levelFailedPopupPresenter = _presenterFactory.Create();
            _popup.Show(levelFailedPopupPresenter);
        }
    }
}
