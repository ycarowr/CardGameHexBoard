using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.GameBoard;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace HexCardGame.UI
{
    public class UiBoard : UiEventListener, ICreateBoard<CreatureElement>, IRestartGame
    {
        [SerializeField] TileBase test;
        IBoard<CreatureElement> CurrentBoard { get; set; }
        Tilemap TileMap { get; set; }

        void ICreateBoard<CreatureElement>.OnCreateBoard(IBoard<CreatureElement> board)
        {
            CurrentBoard = board;
            CreateBoardUi();
        }

        void IRestartGame.OnRestart() => TileMap.ClearAllTiles();

        protected override void Awake()
        {
            base.Awake();
            TileMap = GetComponentInChildren<Tilemap>();
        }

        void CreateBoardUi()
        {
            foreach (var i in CurrentBoard.Positions)
            {
                var cell = new Vector3Int(i.x, i.y, 0);
                TileMap.SetTile(cell, test);
                TileMap.CellToWorld(i);
            }
        }
    }
}