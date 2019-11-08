using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.Game;
using UnityEngine;
using UnityEngine.Tilemaps;
using Logger = Tools.Logger;

namespace HexCardGame.UI
{
    public class UiBoardElements : UiEventListener, ICreateBoardElement
    {
        Tilemap TileMap { get; set; }

        void ICreateBoardElement.OnCreateBoardElement(PlayerId id, BoardElement boardElement, Vector2Int position,
            CardHand card)
        {
            var worldPosition = GetWorldPosition(position);
            Logger.Log<UiBoard>("Create Board Element at: " + worldPosition);
            TileMap.SetTile((Vector3Int) position, boardElement.Data.Tile);
        }

        protected override void Awake()
        {
            base.Awake();
            TileMap = GetComponentInChildren<Tilemap>();
        }

        Vector3 GetWorldPosition(Vector2Int position) => TileMap.GetCellCenterWorld((Vector3Int) position);
    }
}