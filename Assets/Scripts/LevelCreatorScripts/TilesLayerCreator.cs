using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LevelCreatorScripts
{
    [Serializable]
    public class TilesLayerCreator : MonoBehaviour
    {
        private GameObject _object;
        private int _index = 0;

        [SerializeField] [PropertyOrder(2)] private List<TileCreator> _layers;
    
        [Button("Create Layer"), PropertyOrder(1)]
        private void CreateTreeCreator()
        {
            CreateLayer();
        }
    
        private TileCreator CreateLayer()
        {
            _layers = FindObjectsOfType<TileCreator>().ToList();
            _index = _layers.Count;
            GameObject obj = (GameObject)Resources.Load("Tiles/TileLayer");
            _object = Object.Instantiate(obj, transform);
            TileCreator tileCreator = _object.GetComponent<TileCreator>();
            _layers.Add(tileCreator);
            tileCreator.Construct(_index);
            _object.name = $"{_index}";

            return tileCreator;
        }

        public SerializableTree CreateSerializableTree()
        {
            SerializableTree tree = new SerializableTree();

            _layers = FindObjectsOfType<TileCreator>().ToList();
            tree.Layers = new();
            _layers.ForEach(layer => tree.Layers.Add(layer.CreateSerializableLayer()));

            return tree;
        }

        public void LoadTree(SerializableTree tree)
        {
            foreach (TileCreator tileCreator in _layers)
            {
                if(tileCreator == null)
                    continue;
            
                DestroyImmediate(tileCreator.gameObject);
            }
        
            _layers.Clear();
        
            foreach (TileCreator.SerializableTileLayer layer in tree.Layers)
            {
                TileCreator tileCreator = CreateLayer();
                tileCreator.LoadTiles(layer.Tiles);
            }
        
        
        }

        [Serializable]
        public class SerializableTree
        {
            public List<TileCreator.SerializableTileLayer> Layers;
        
        }
    }
}