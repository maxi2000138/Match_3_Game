using Popups.LevelComplete;
using Popups.LevelFailed;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class PopupsInstaller : MonoInstaller
    {
        [SerializeField] private LevelFailedPopup _levelFailedPopup;
        [SerializeField] private LevelCompletePopup _levelCompletePopup;
    
        public override void InstallBindings()
        {
            BindLevelFailedPopup();
            BindLevelCompletePopup();
        }
    
        private void BindLevelCompletePopup()
        {
            Container
                .Bind<LevelCompletePopup>()
                .FromInstance(_levelCompletePopup)
                .AsSingle();

            Container
                .BindFactory<ILevelCompletePopupPresenter, LevelCompletePopupPresenter.Factory>()
                .To<LevelCompletePopupPresenter>()
                .AsSingle();

            Container
                .Bind<LevelCompletePopupShower>()
                .AsSingle()
                .NonLazy();
        }

        private void BindLevelFailedPopup()
        {
            Container
                .Bind<LevelFailedPopup>()
                .FromInstance(_levelFailedPopup)
                .AsSingle();

            Container
                .BindFactory<ILevelFailedPopupPresenter, LevelFailedPopupPresenter.Factory>()
                .To<LevelFailedPopupPresenter>()
                .AsSingle();

            Container
                .Bind<LevelFailedPopupShower>()
                .AsSingle()
                .NonLazy();
        }
    }
}
