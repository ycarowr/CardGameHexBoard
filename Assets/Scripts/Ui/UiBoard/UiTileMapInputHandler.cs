using Tools.Patterns.Observer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

namespace HexCardGame.UI
{
    public interface ITileMapInput
    {
        bool IsLocked { get; }
        void Lock();
        void Unlock();
    }

    [Event]
    public interface IOnClickTile
    {
        void OnClickTile(Vector3Int position);
    }

    [RequireComponent(typeof(Tilemap))]
    [RequireComponent(typeof(TilemapCollider2D))]
    public class UiTileMapInputHandler : MonoBehaviour, ITileMapInput, IPointerClickHandler
    {
        IDispatcher Dispatcher { get; set; }
        Tilemap TileMap { get; set; }
        Camera Camera { get; set; }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (IsLocked)
                return;
            var worldPoint = ScreenToWorldPosition(eventData.position);
            var position = TileMap.WorldToCell(worldPoint);
            Dispatcher.Notify<IOnClickTile>(i => i.OnClickTile(position));
        }

        public void Lock() => IsLocked = true;
        public void Unlock() => IsLocked = false;
        public bool IsLocked { get; private set; }

        void Awake()
        {
            Camera = Camera.main;
            TileMap = GetComponentInChildren<Tilemap>();
            Dispatcher = EventsDispatcher.Load();
        }

        Vector3 ScreenToWorldPosition(Vector3 point) => Camera.ScreenToWorldPoint(point);
    }
}