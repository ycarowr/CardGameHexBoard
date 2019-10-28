using System;
using System.Collections;
using System.Collections.Generic;
using HexCardGame;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Ui
{
    public class UiBoardDebug : UiEventListener, ICreateBoard
    {
        List<TMP_Text> textPositions = new List<TMP_Text>();
        [SerializeField] GameObject textPosition;
        Tilemap TileMap { get; set; }
        Board CurrentBoard { get; set; }


        protected override void Awake()
        {
            base.Awake();
            TileMap = GetComponentInChildren<Tilemap>();
        }

        void ICreateBoard.OnCreateBoard(Board board)
        {
            CurrentBoard = board;
            DrawPositions();
        }

        [Button]
        void DrawPositions()
        {
            const string uiPosition = "UiPosition_";
            var identity = Quaternion.identity;
            foreach (var p in CurrentBoard.Units)
            {
                var worldPosition = TileMap.CellToWorld(p);
                var gameObj = Instantiate(textPosition, worldPosition, identity, transform);
                var tmpText = gameObj.GetComponent<TMP_Text>();
                var vector2Int = p.AsVector2Int();
                var sPosition = $"y:{vector2Int.y}\nx:{vector2Int.x}";
                tmpText.text = sPosition;
                tmpText.name = uiPosition + sPosition;
                textPositions.Add(tmpText);
            }
        }
    }
}
