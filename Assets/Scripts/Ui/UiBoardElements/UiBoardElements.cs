using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.Game;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace HexCardGame.UI
{
    public class UiBoardElements : UiEventListener, ICreateBoardElement, IRestartGame
    {
        Tilemap TileMap { get; set; }

        void ICreateBoardElement.OnCreateBoardElement(PlayerId id, CreatureElement creatureElement, Vector3Int position,
            CardHand card) =>
            TileMap.SetTile(position, creatureElement.Data.Tile);

        void IRestartGame.OnRestart() => TileMap.ClearAllTiles();

        protected override void Awake()
        {
            base.Awake();
            TileMap = GetComponentInChildren<Tilemap>();
        }
    }
}