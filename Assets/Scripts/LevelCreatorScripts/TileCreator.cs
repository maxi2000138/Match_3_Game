using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Types;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using Vector2 = UnityEngine.Vector2;

namespace LevelCreatorScripts
{
    [Serializable]
    public class TileCreator : MonoBehaviour
    {
        private int _index;
        private List<TileTemplate> _tiles;

        public int Index =>
            _index;

        public void Construct(int index)
        {
            _index = index;
        }

        [Button("Create Node"), PropertyOrder(0)]
        private void CreateNodeCreator(TileType tileType, Vector2 position = new())
        {
            if (_tiles == null) 
                _tiles = GameObject.FindObjectsOfType<TileTemplate>().ToList();
        
            GameObject obj = (GameObject)Resources.Load("Tiles/TileTemplate");
            GameObject spawnedObj = Object.Instantiate(obj, transform);
            spawnedObj.transform.position = Camera.main.ViewportToScreenPoint(position);
            TileTemplate tileTemplate = spawnedObj.GetComponent<TileTemplate>();
            tileTemplate.TileType = tileType;
            tileTemplate.SetColor();
            Selection.activeObject = spawnedObj;
            _tiles.Add(tileTemplate);
        }

        public void LoadTiles(List<TileTemplate.SerializableTile> tiles)
        {
            if (_tiles != null)
                _tiles.Clear();
            else
                _tiles = new();

            foreach (TileTemplate.SerializableTile tile in tiles) 
                CreateNodeCreator(tile.TileType, tile.ViewportPosition);
        }

        public SerializableTileLayer CreateSerializableLayer()
        {
            SerializableTileLayer layer = new SerializableTileLayer();
            layer.Tiles = new();

            _tiles = GetComponentsInChildren<TileTemplate>().ToList();


            foreach (TileTemplate tile in _tiles)
            {
                if(tile == null)
                    continue;

                layer.Tiles.Add(tile.CreateSerializableTile());
            }
        
            layer.LayerIndex = _index;
            return layer;
        }
    
        [Serializable]
        public class SerializableTileLayer
        {
            public int LayerIndex;
            public List<TileTemplate.SerializableTile> Tiles;
        }
    }
}