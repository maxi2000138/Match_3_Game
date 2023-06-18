using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.LevelFailed
{
    public class LevelFailedPopup : MonoBehaviour, IPopup
    {
        private ILevelFailedPopupPresenter _presenter;

        [SerializeField] private TMP_Text _mainText;
        [SerializeField] private TMP_Text _playAgainText;
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private GameObject _popup;
    
        public void Show(object args)
        {
            if (args is not ILevelFailedPopupPresenter presenter)
            {
                throw new Exception("Expected Level Failed Popup Presenter");
            }

            _presenter = presenter;
            _mainText.text = _presenter.MainText;
            _playAgainText.text = _presenter.PlayAgainText;
            _playAgainButton.onClick.AddListener(_presenter.OnPlayAgainButtonClick);
        

            _popup.transform.localScale = new Vector3(0.1f,0.1f,1f);
            _popup.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutElastic);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _playAgainButton.onClick.RemoveListener(_presenter.OnPlayAgainButtonClick);
            gameObject.SetActive(false);
        }
    }
}