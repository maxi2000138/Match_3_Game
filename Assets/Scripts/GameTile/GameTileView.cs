using System;
using Infrastructure.Services;
using Types;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace GameTile
{
    [RequireComponent(typeof(Button))]
    public class GameTileView : MonoBehaviour
    {
        private StaticDataService _staticDataService;
        private Button _button;
        private UnityAction _onClick;
        
        [SerializeField] private Image _icon;

        public event Action OnTileClick;

        [Inject]
        public void Construct(StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void EnableView(TileType tileType, UnityAction onTileClick)
        {
            _onClick = onTileClick;
            _icon.color = Color.white;
            _icon.sprite = _staticDataService.GetTileData(tileType).Sprite;
            _button.onClick.AddListener(_onClick);
        }

        public void DisableView()
        {
            _button.onClick.RemoveListener(_onClick);
        }

        public void EnableTile()
        {
            _button.interactable = true;
        }

        public void DisableTile()
        {
            _button.interactable = false;
        }
    }
}