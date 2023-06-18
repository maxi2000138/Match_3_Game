using Types;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = ("Create tile static data"), fileName = "new tile")]
    public class TileStaticData : ScriptableObject
    {
        public Sprite Sprite;
        public TileType TileType;
    }
}
