using HexCardGame.Model.GameBoard;
using UnityEngine;
using UnityEngine.Tilemaps;
using Logger = Tools.Logger;

namespace Game.Ui
{
    public class UiBoard : UiEventListener, ICreateBoard
    {
        GameObject[] positions;
        [SerializeField] TileBase test;
        IBoard CurrentBoard { get; set; }
        Tilemap TileMap { get; set; }

        void ICreateBoard.OnCreateBoard(IBoard board)
        {
            CurrentBoard = board;
            DrawUi();
        }

        protected override void Awake()
        {
            base.Awake();
            TileMap = GetComponentInChildren<Tilemap>();
        }

        [Button]
        void DrawUi()
        {
            Logger.Log<UiBoard>("Board View Created");
            foreach (var p in CurrentBoard.Positions)
                TileMap.SetTile(p, test);
        }

        [Button]
        void ClearUi() => TileMap.ClearAllTiles();
    }
}