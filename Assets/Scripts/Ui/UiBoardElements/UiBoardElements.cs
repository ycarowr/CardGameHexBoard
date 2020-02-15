using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.Game;
using HexCardGame.SharedData;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace HexCardGame.UI
{
    public class UiBoardElements : UiEventListener, ICreateBoardElement, IRestartGame
    {
        [SerializeField] GameObject boardElementPrefab;
        Tilemap TileMap { get; set; }
        protected override void Awake()
        {
            base.Awake();
            TileMap = GetComponentInChildren<Tilemap>();
        }

        void ICreateBoardElement.OnCreateBoardElement(PlayerId id, CreatureElement creatureElement, Vector3Int cell, CardHand card)
        {
            var data = creatureElement.Data;
            CreateElement(creatureElement, data, cell);
        }

        void CreateElement(CreatureElement creatureElement, ICardData data, Vector3Int cell)
        {
            var element = ObjectPooler.Instance.Get(boardElementPrefab);
            var elementTransform = element.transform;
            var boardElement = element.GetComponent<UiBoardElement>();
            elementTransform.SetParent(transform);
            elementTransform.position = TileMap.CellToWorld(cell);
            boardElement.SetElement(creatureElement);
        }

        void IRestartGame.OnRestart() => TileMap.ClearAllTiles();

    }
}