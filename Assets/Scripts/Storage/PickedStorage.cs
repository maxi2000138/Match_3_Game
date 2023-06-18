using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameTile;
using Infrastructure.Services;
using Popups.LevelComplete;
using Popups.LevelFailed;

namespace Storage
{
    public class PickedStorage
    {
        private readonly StackChecker _stackChecker;
        private readonly TileDestroyer _tileDestroyer;
        private readonly LooseWinCheacker _looseWinCheacker;
        private readonly LevelFailedPopupShower _failedPopupShower;
        private readonly LevelCompletePopupShower _completePopupShower;

        private readonly LinkedList<GameTile.GameTile> _pickedTiles = new();

        private readonly TileMover _tileMover;
        private GameTile.GameTile _gameTile;

        public PickedStorage(TileMover tileMover, StackChecker stackChecker, TileDestroyer tileDestroyer
            , LooseWinCheacker looseWinCheacker, LevelFailedPopupShower failedPopupShower, LevelCompletePopupShower completePopupShower)
        {
            _tileMover = tileMover;
            _stackChecker = stackChecker;
            _tileDestroyer = tileDestroyer;
            _looseWinCheacker = looseWinCheacker;
            _failedPopupShower = failedPopupShower;
            _completePopupShower = completePopupShower;
        }
    
        public async void AddTile(GameTile.GameTile tile)
        {
            _gameTile = _pickedTiles.LastOrDefault(f => f.TileType == tile.TileType);
            List<GameTile.GameTile> deletedTiles;
        
            if (_gameTile != null)
                _pickedTiles.AddAfter(_pickedTiles.Find(_gameTile), tile);
            else
                _pickedTiles.AddLast(tile);

            _tileMover.MoveAll(_pickedTiles);
        
        
            if (_stackChecker.CanDeleteStack(_pickedTiles, out deletedTiles))
            {
                await Task.Delay((int)(_tileMover.FlyDuration*1000));
            
                foreach (GameTile.GameTile obj in deletedTiles) 
                    _pickedTiles.Remove(obj);
            
                _tileDestroyer.DestroyTiles(deletedTiles);
            }
            else
            {
                await Task.Delay((int)(_tileMover.FlyDuration*1000));
            }

            WinLooseCheck();
            _tileMover.MoveAll(_pickedTiles);
            await Task.Delay((int)(_tileMover.FlyDuration*1000));
            WinLooseCheck();
        }

        private void WinLooseCheck()
        {
            if (_looseWinCheacker.CheckLoose(_pickedTiles))
                _failedPopupShower.Show();

            else if (_looseWinCheacker.CheckWin())
                _completePopupShower.Show();
        }
    }
}