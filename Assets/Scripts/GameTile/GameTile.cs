using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Storage;
using Types;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameTile
{
    public class GameTile
    {
        private readonly PickedStorage _pickedStorage;
        private readonly GameFactory _gameFactory;
        private readonly TileType _tileType;
        private bool _isActive = true;
        private GameTileView _tileView;
        private List<GameTile> _overlayByTiles;

        public event Action OnClicked;

        public GameTileView TileView =>
            _tileView;

        public TileType TileType =>
            _tileType;
        public bool IsActive =>
            _isActive;

        public GameTile(TileType tileType, PickedStorage pickedStorage, GameFactory gameFactory)
        {
            _pickedStorage = pickedStorage;
            _tileType = tileType;
            _gameFactory = gameFactory;
        }

        public void SetupOverlayedTiles(List<GameTile> overlayedByTiles)
        {
            if (overlayedByTiles == null)
            {
                Debug.LogError("Overlayed tiles are null!");
                return;
            }
            if (_overlayByTiles != null)
            {
                Debug.LogError("Overlayed tiles already setuped!");
                return;
            }

            _overlayByTiles = overlayedByTiles;
        }

        public void Enable()
        {
            _isActive = true;
            _tileView.EnableView(_tileType, OnTileClick);
            TryUpdateTileState();
        }

        public void Disable()
        {
            _isActive = false;
            _tileView.DisableView();
            DestroyView();
        }
        
        private void OnTileClick()
        {
            _isActive = false;
            _tileView.DisableTile();
            _tileView.DisableView();
            
            _pickedStorage.AddTile(this);
            
            OnClicked?.Invoke();
        }

        public void TryUpdateTileState()
        {
            if(!_isActive)
                return;
            
            if (_overlayByTiles != null)
            {
                foreach (GameTile tile in _overlayByTiles)
                {
                    if (tile != null && tile.IsActive)
                    {
                        _tileView.DisableTile();
                        return;
                    }
                }
            }
            
            _tileView.EnableTile();
        }

        public void CreateView(Transform parent, Vector2 viewportPosition)
        {
            if (_tileView != null)
            {
                Debug.LogError("View is already spawned!");
                return;
            }
            
            _tileView = _gameFactory.SpawnTileView(parent, viewportPosition);
        }

        private void DestroyView()
        {
            if(_tileView != null)
                Object.Destroy(_tileView.gameObject);
        }
        
        public class Factory : PlaceholderFactory<TileType ,GameTile>
        {
            
        }
    }
    
    
}