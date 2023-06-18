using System.Collections.Generic;
using System.Linq;
using GameTile;

namespace Infrastructure.Services
{
    public class LooseWinCheacker
    {
        private TileMover _tileMover;
        private readonly TileClickEventer _tileClickEventer;
        private int _maxTileCount = 7;

        public int MaxTileCount =>
            _maxTileCount;
    
        public LooseWinCheacker(TileMover tileMover, TileClickEventer tileClickEventer)
        {
            _tileMover = tileMover;
            _tileClickEventer = tileClickEventer;
        }

        public bool CheckWin() => 
            _tileClickEventer.IsAllTilesDisabled && !_tileMover.IsAnyoneMoved;

        public bool CheckLoose(LinkedList<GameTile.GameTile> tiles) =>
            tiles.Count() >= 7 && !_tileMover.IsAnyoneMoved;
    }
}
