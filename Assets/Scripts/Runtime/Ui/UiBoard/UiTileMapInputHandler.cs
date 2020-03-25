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
        void OnClickTile(Vector3Int cell);
    }

    [Event]
    public interface IOnRightClickTile
    {
        void OnRightClickTile(Vector3Int cell, Vector2 screenPosition);
    }

    [RequireComponent(typeof(IMouseInput)), RequireComponent(typeof(Tilemap)),
     RequireComponent(typeof(TilemapCollider2D))]
    public class UiTileMapInputHandler : MonoBehaviour, ITileMapInput
    {
        private IMouseInput Input { get; set; }
        private IDispatcher Dispatcher { get; set; }
        private Tilemap TileMap { get; set; }
        private Camera Camera { get; set; }

        public void Lock()
        {
            Input.StopTracking();
        }

        public void Unlock()
        {
            Input.StartTracking();
        }

        public bool IsLocked => Input.IsTracking;

        private void OnPointerClick(PointerEventData eventData)
        {
            if (IsLocked)
                return;
            var screenPosition = eventData.position;
            var cell = ConvertPixelToCell(screenPosition);

            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    Dispatcher.Notify<IOnClickTile>(i => i.OnClickTile(cell));
                    break;
                case PointerEventData.InputButton.Right:
                    Dispatcher.Notify<IOnRightClickTile>(i => i.OnRightClickTile(cell, screenPosition));
                    break;
            }
        }

        private void Awake()
        {
            Camera = Camera.main;
            TileMap = GetComponentInChildren<Tilemap>();
            Dispatcher = EventsDispatcher.Load();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerClick += OnPointerClick;
        }

        private Vector3Int ConvertPixelToCell(Vector2 screenPoint)
        {
            var worldPosition = Camera.ScreenToWorldPoint(screenPoint);
            var cell = TileMap.WorldToCell(worldPosition);
            return cell;
        }
    }
}