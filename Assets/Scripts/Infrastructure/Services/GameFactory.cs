using GameTile;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services
{
    public class GameFactory
    {
        private readonly DiContainer _diContainer;

        public GameFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
    
        public GameTileView SpawnTileView(Transform parent, Vector2 viewportPosition)
        {
            GameObject tileObject = (GameObject)Resources.Load(ResourcePathes.GameTileViewPath);
            GameObject spawnedTile = _diContainer.InstantiatePrefab(tileObject, Camera.main.ViewportToScreenPoint(viewportPosition),quaternion.identity, parent);
            return spawnedTile.GetComponent<GameTileView>();    
        }

        public GameObject SpawnTileLayer(Transform parent)
        {
            GameObject layerObject = (GameObject)Resources.Load(ResourcePathes.GameTileLayerPath);
            GameObject spawnedLayer = _diContainer.InstantiatePrefab(layerObject, parent);
            return spawnedLayer;
        }
    }
}
