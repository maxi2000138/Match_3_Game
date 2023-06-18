using Sirenix.OdinInspector;
using StaticData;
using UnityEngine;

namespace LevelCreatorScripts
{
    public class EditorLevelSaverLoader : MonoBehaviour
    {
        private TilesLayerCreator _layerCreator;
    
        [SerializeField] private LevelInformationData LevelInformationData;

        [Button]
        public void Save()
        {
            if(_layerCreator == null)
                _layerCreator = GameObject.FindObjectOfType<TilesLayerCreator>();
        
            TilesLayerCreator.SerializableTree tree = _layerCreator.CreateSerializableTree();
            LevelInformationData.TilesData = tree;
            UnityEditor.EditorUtility.SetDirty(LevelInformationData);
            Debug.Log("<color=green>Data Saved!</color>");
        }

        [Button]
        public void Load()
        {
            if (LevelInformationData == null)
            {
                Debug.Log("<color=red>Level Information is empty!</color>");
                return;
            }
        
            if(_layerCreator == null)
                _layerCreator = GameObject.FindObjectOfType<TilesLayerCreator>();
        
            _layerCreator.LoadTree(LevelInformationData.TilesData);
            Debug.Log("<color=green>Data Loaded!</color>");
        }
    }
}
