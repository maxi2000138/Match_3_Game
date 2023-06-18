using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services
{
    public class TilesInitializer
    {
        private Dictionary<int, List<GameTile.GameTile>> _tilesByIndex;

        public void Construct(Dictionary<int, List<GameTile.GameTile>> tilesByIndex)
        {
            if (_tilesByIndex != null)
            {
                Debug.LogError("Tiles already Initailized!");
                return;
            }
        
            _tilesByIndex = tilesByIndex;
        }

        public void InitializeTiles()
        {
            for (int i = 0; i < _tilesByIndex.Count; i++)
            {
                _tilesByIndex[i].ForEach(tile => tile.Enable());
            }   
        }
    }
}

