using HexCardGame.Model.GameBoard;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Ui
{
    public class UiBoardDebug : UiEventListener, ICreateBoard
    {
        GameObject[] positions;
        [SerializeField] GameObject textPosition;
        Tilemap TileMap { get; set; }
        IBoard CurrentBoard { get; set; }

        void ICreateBoard.OnCreateBoard(IBoard board)
        {
            CurrentBoard = board;
            DrawPositions();
        }


        protected override void Awake()
        {
            base.Awake();
            TileMap = GetComponentInChildren<Tilemap>();
        }

        [Button]
        void DrawPositions()
        {
            const string uiPosition = "UiPosition_";
            var identity = Quaternion.identity;
            ClearPositions();
            positions = new GameObject[CurrentBoard.Positions.Count];
            for (var i = 0; i < CurrentBoard.Positions.Count; i++)
            {
                var p = CurrentBoard.Positions[i];
                var worldPosition = TileMap.CellToWorld(p);
                var gameObj = Instantiate(textPosition, worldPosition, identity, transform);
                positions[i] = gameObj;
                var tmpText = gameObj.GetComponent<TMP_Text>();
                var vector2Int = p.AsVector2Int();
                var sPosition = $"y:{vector2Int.y}\nx:{vector2Int.x}";
                tmpText.text = sPosition;
                tmpText.name = uiPosition + sPosition;
            }
        }

        [Button]
        void ClearPositions()
        {
            if (positions == null)
                return;

            foreach (var i in positions)
                Destroy(i);
        }

        [Button]
        void RegenerateBoard()
        {
            TileMap.ClearAllTiles();
            CurrentBoard?.Clear();
            CurrentBoard?.GeneratePositions();
        }
    }
}