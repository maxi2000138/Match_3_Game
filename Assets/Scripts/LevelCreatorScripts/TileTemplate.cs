using System;
using System.Collections.Generic;
using System.Linq;
using Ids;
using Types;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreatorScripts
{
    [ExecuteInEditMode]
    public class TileTemplate : MonoBehaviour
    {
        public TileType TileType;
        public Image Image;

        [SerializeField] public List<TileTemplate> _overlayedObjects;


        private void OnValidate() => 
            SetColor();

        public void SetColor() => 
            Image.color = GetColor(TileType);

        private Color GetColor(TileType tileType)
        {
            switch (tileType)
            {
                case (TileType.RED):
                {
                    return Color.red;
                }
                case (TileType.BLUE):
                {
                    return Color.blue;
                }
                case (TileType.PURPLE):
                {
                    return Color.magenta;
                }
                case (TileType.GREEN):
                {
                    return Color.green;
                }
                case (TileType.YELLOW):
                {
                    return Color.yellow;
                }
                case (TileType.ORANGE):
                {
                    return new Color32(255,114,0,255);
                }
                default:
                {
                    return Color.white;
                }
            }
        }

        private void Update()
        {
            MakeTransparent();
        }
    
        public void DrawArrow(Vector2 pos, Vector2 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Debug.DrawRay(pos, direction);
       
            Vector2 right = Quaternion.LookRotation(direction) * Quaternion.Euler(180+arrowHeadAngle,0,0) * new Vector3(0,0,1);
            Vector2 left = Quaternion.LookRotation(direction) * Quaternion.Euler(180-arrowHeadAngle,0 ,0 ) * new Vector3(0,0,1);
            Debug.DrawRay(pos + direction, right * arrowHeadLength);
            Debug.DrawRay(pos + direction, left * arrowHeadLength);
        }
        private void MakeTransparent()
        {
            if (Selection.activeGameObject != gameObject)
            {
                Color color = Image.color;
                color.a = 0.4f;
                Image.color = color;
            }
            else
            {
                Color color = Image.color;
                color.a = 0.8f;
                Image.color = color;
                foreach (TileTemplate tileTemplate in _overlayedObjects)
                {
                    DrawArrow(transform.position, tileTemplate.transform.position - transform.position,50);            
                }
            }
        }

        public SerializableTile CreateSerializableTile()
        {
            SerializableTile tile = new()
            {
                TileType = TileType,
                ViewportPosition = Camera.main.ScreenToViewportPoint(transform.position),
                ObjectId = GetComponent<UniqueId>().Id,
                OverlayedIds = _overlayedObjects.Select(obj => obj.GetComponent<UniqueId>().Id).ToList(),
            };
        
            return tile;
        }
    
        [Serializable]
        public class SerializableTile
        {
            public TileType TileType;
            public Vector2 ViewportPosition;
            public string ObjectId;
            public List<string> OverlayedIds;
        }
    }
}
