using Tools.Input.Mouse;
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

    [RequireComponent(typeof(IMouseInput)), RequireComponent(typeof(Tilemap)),
     RequireComponent(typeof(TilemapCollider2D))]
    public class UiTileMapInputHandler : MonoBehaviour, ITileMapInput
    {
        IMouseInput Input { get; set; }
        IDispatcher Dispatcher { get; set; }
        Tilemap TileMap { get; set; }
        Camera Camera { get; set; }

        public void Lock() => Input.StopTracking();
        public void Unlock() => Input.StartTracking();
        public bool IsLocked => Input.IsTracking;

        void OnPointerClick(PointerEventData eventData)
        {
            if (IsLocked)
                return;
            var worldPoint = ScreenToWorldPosition(eventData.position);
            var position = TileMap.WorldToCell(worldPoint);
            Dispatcher.Notify<IOnClickTile>(i => i.OnClickTile(position));
        }

        void Awake()
        {
            Camera = Camera.main;
            TileMap = GetComponentInChildren<Tilemap>();
            Dispatcher = EventsDispatcher.Load();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerClick += OnPointerClick;
        }

        Vector3 ScreenToWorldPosition(Vector3 point) => Camera.ScreenToWorldPoint(point);
    }
}