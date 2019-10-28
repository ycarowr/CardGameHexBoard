using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Ui
{
    [CreateAssetMenu(menuName = "Tiles/Test")]
    public class TestTile : TileBase
    {
        [SerializeField] Sprite sprite;
    }
}