using System.Collections.Generic;
using System.Linq;
using LevelCreatorScripts;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services
{
    public class TilesSpawner : MonoBehaviour
    {
        private Dictionary<string, GameTile.GameTile> _tilesById;
        private Dictionary<int, List<GameTile.GameTile>> _tilesByIndex;
        private LevelInformationData _levelInformationData;
        private GameFactory _gameFactory;
        private GameTile.GameTile.Factory _tileFactory;
        private TilesInitializer _tilesInitializer;
        private TileClickEventer _tileClickEventer;
        private LevelDataLoader _levelDataLoader;

        [Inject]
        public void Construct(LevelDataLoader levelDataLoader, GameFactory gameFactory
            , GameTile.GameTile.Factory tileFactory, TilesInitializer tilesInitializer, TileClickEventer tileClickEventer)
        {
            _levelDataLoader = levelDataLoader;
            _tileClickEventer = tileClickEventer;
            _tilesInitializer = tilesInitializer;
            _tileFactory = tileFactory;
            _gameFactory = gameFactory;
        }


        public void InitTileInitializer() => 
            _tilesInitializer.Construct(_tilesByIndex);

        public void InitTileClickEventer() => 
            _tileClickEventer.Construct(_tilesByIndex.SelectMany(tile => tile.Value).ToList());
    
        public void Spawn()
        {
            _levelInformationData = _levelDataLoader.LevelInformationData;
        
            if (_levelInformationData == null)
            {
                Debug.LogError("Level Information data is null!");
            }

            _tilesByIndex = new();
            _tilesById = new();
            Dictionary<string, List<GameTile.GameTile>> overlayedByTiles = new();

            foreach (TileCreator.SerializableTileLayer layer in _levelInformationData.TilesData.Layers)
            {
                GameObject spawnedLayer = _gameFactory.SpawnTileLayer(transform);
                spawnedLayer.name = layer.LayerIndex.ToString();

                foreach (TileTemplate.SerializableTile tile in layer.Tiles)
                {
                    GameTile.GameTile gameTile = _tileFactory.Create(tile.TileType);
                
                    if (!_tilesByIndex.ContainsKey(layer.LayerIndex))
                        _tilesByIndex[layer.LayerIndex] = new();

                    _tilesByIndex[layer.LayerIndex].Add(gameTile);
                
                    gameTile.CreateView(spawnedLayer.transform, tile.ViewportPosition);
                    _tilesById.Add(tile.ObjectId, gameTile);
                }
            }
        
            foreach (TileCreator.SerializableTileLayer layer in _levelInformationData.TilesData.Layers)
            {
                foreach (TileTemplate.SerializableTile tile in layer.Tiles)
                {
                    foreach (string tileOverlayedId in tile.OverlayedIds)
                    {
                        if (!overlayedByTiles.ContainsKey(tileOverlayedId))
                            overlayedByTiles[tileOverlayedId] = new();
                    
                        overlayedByTiles[tileOverlayedId].Add(_tilesById[tile.ObjectId]);
                    }
                }
            }

            foreach (string key in overlayedByTiles.Keys) 
                _tilesById[key].SetupOverlayedTiles(overlayedByTiles[key]);
        }
    }
}