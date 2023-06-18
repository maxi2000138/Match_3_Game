using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.Services
{
    public class TileClickEventer
    {
        private List<GameTile.GameTile> _tiles;

        public bool IsAllTilesDisabled
            => _tiles.All(tile => !tile.IsActive);

        public void Construct(List<GameTile.GameTile> tiles)
        {
            if (_tiles != null)
            {
                Debug.LogError("Tiles already Initailized!");
                return;
            }
        
            _tiles = tiles;
        }

        public void InitializeEvents()
        {
            _tiles.ForEach(tile => tile.OnClicked += OnAnyTileClick);
        }

        public void OnAnyTileClick()
        {
            foreach (GameTile.GameTile tile in _tiles) 
                tile.TryUpdateTileState();
        }
    }
}
