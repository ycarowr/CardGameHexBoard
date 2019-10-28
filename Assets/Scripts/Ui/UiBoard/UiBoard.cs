using System;
using System.Collections;
using System.Collections.Generic;
using HexCardGame;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Ui
{
    public class UiBoard : UiEventListener, ICreateBoard
    {
        [SerializeField] TileBase test;
        Board CurrentBoard { get; set; }
        Tilemap TileMap { get; set; }

        protected override void Awake()
        {
            base.Awake();
            TileMap = GetComponentInChildren<Tilemap>();
        }

        void ICreateBoard.OnCreateBoard(Board board)
        {
            CurrentBoard = board;
            Debug.Log("Ui boar View created");
            foreach(var p in board.Units)
                TileMap.SetTile(p, test);
        }
    }
}
