using System.Collections.Generic;
using System.Linq;
using StaticData;
using Types;
using UnityEngine;

namespace Infrastructure.Services
{
    public class StaticDataService
    {
        private const string _tilesDataPath = "StaticData/Tiles";
        private const string _levelsDataPath = "StaticData/Levels";
    
        private Dictionary<TileType, TileStaticData> _tilesData;
        private Dictionary<int, LevelInformationData> _levelsData;

        public void Load()
        {
            _tilesData = Resources
                .LoadAll<TileStaticData>(_tilesDataPath)
                .ToDictionary(x => x.TileType, x => x);
        
            _levelsData = Resources
                .LoadAll<LevelInformationData>(_levelsDataPath)
                .ToDictionary(x => x.LevelNumber, x => x);
        }

        public int LevelsAmount =>
            _levelsData.Count;

        public TileStaticData GetTileData(TileType tileType) =>
            _tilesData.TryGetValue(tileType, out TileStaticData tileStaticData)
                ? tileStaticData
                : null;

        public LevelInformationData GetLevelData(int lvlNumber) =>
            _levelsData.TryGetValue(lvlNumber, out LevelInformationData levelInformationData)
                ? levelInformationData
                : null;
    }
}
