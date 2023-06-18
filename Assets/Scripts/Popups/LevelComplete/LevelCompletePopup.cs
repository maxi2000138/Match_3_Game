using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.LevelComplete
{
    public class LevelCompletePopup : MonoBehaviour, IPopup
    {
        private ILevelCompletePopupPresenter _presenter;

        [SerializeField] private TMP_Text _mainText;
        [SerializeField] private TMP_Text _playAgainText;
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private TMP_Text _nextLevelText;
        [SerializeField] private Button _nextLevelButton;
    
        public void Show(object args)
        {
            if (args is not ILevelCompletePopupPresenter presenter)
            {
                throw new Exception("Expected Level Complete Popup Presenter");
            }

            _presenter = presenter;
            _mainText.text = _presenter.MainText;
            _playAgainText.text = _presenter.PlayAgainText;
            _nextLevelText.text = _presenter.NextLevelText;
            _playAgainButton.onClick.RemoveAllListeners();
            _nextLevelButton.onClick.RemoveAllListeners();
            _playAgainButton.onClick.AddListener(_presenter.OnPlayAgainButtonClick);
            _nextLevelButton.onClick.AddListener(_presenter.OnNextLevelButtonClick);
        
            gameObject.SetActive(true);
        
        }

        public void Hide()
        {
            _playAgainButton.onClick.RemoveListener(_presenter.OnPlayAgainButtonClick);
            _nextLevelButton.onClick.RemoveListener(_presenter.OnNextLevelButtonClick);
            gameObject.SetActive(false);
        }
    }
}