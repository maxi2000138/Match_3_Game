using LevelCreatorScripts;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "Create Level Data", fileName = "New Level Data")]
    public class LevelInformationData : ScriptableObject
    {
        public int LevelNumber;
        public TilesLayerCreator.SerializableTree TilesData;
    }
}
