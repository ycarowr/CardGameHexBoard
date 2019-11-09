using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.GameBoard;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using Logger = Tools.Logger;

namespace HexCardGame.UI
{
    public class UiBoard : UiEventListener, ICreateBoard<BoardElement>, IPointerClickHandler, IRestartGame
    {
        GameObject[] positions;
        [SerializeField] TileBase test;
        IBoard<BoardElement> CurrentBoard { get; set; }
        Tilemap TileMap { get; set; }

        void ICreateBoard<BoardElement>.OnCreateBoard(IBoard<BoardElement> board)
        {
            CurrentBoard = board;
            CreateBoardUi();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }

        void IRestartGame.OnRestart() => TileMap.ClearAllTiles();

        protected override void Awake()
        {
            base.Awake();
            TileMap = GetComponentInChildren<Tilemap>();
        }

        [Button]
        void CreateBoardUi()
        {
            Logger.Log<UiBoard>("Board View Created");
            foreach (var p in CurrentBoard.Positions)
                TileMap.SetTile(p, test);
        }
    }
}