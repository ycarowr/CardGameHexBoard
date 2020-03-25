using HexCardGame.Runtime;
using HexCardGame.Runtime.GameBoard;
using HexCardGame.SharedData;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Ui
{
    public class UiBoardDebug : UiEventListener, ICreateBoard<CreatureElement>
    {
        [SerializeField] private BoardData data;
        private GameObject[] positions;
        [SerializeField] private GameObject textPosition;
        [SerializeField] private Tilemap tileMap;
        private IBoard<CreatureElement> CurrentBoard { get; set; }

        void ICreateBoard<CreatureElement>.OnCreateBoard(IBoard<CreatureElement> board)
        {
            CurrentBoard = board;
            DrawPositions();
        }

        [Button]
        private void DrawPositions()
        {
            const string uiPosition = "UiPosition_";
            var identity = Quaternion.identity;
            ClearPositions();
            positions = new GameObject[CurrentBoard.Positions.Length];
            for (var i = 0; i < CurrentBoard.Positions.Length; i++)
            {
                var p = CurrentBoard.Positions[i];
                var worldPosition = tileMap.CellToWorld(p.Cell);
                var gameObj = Instantiate(textPosition, worldPosition, identity, transform);
                positions[i] = gameObj;
                var tmpText = gameObj.GetComponent<TMP_Text>();
                var sPosition = $"x:{p.x}\ny:{p.y}";
                tmpText.text = sPosition;
                tmpText.name = uiPosition + sPosition;
            }
        }

        [Button]
        private void ClearPositions()
        {
            if (positions == null)
                return;

            foreach (var i in positions)
                Destroy(i);
        }

        [Button]
        private void RegenerateBoard()
        {
            tileMap.ClearAllTiles();
            CurrentBoard?.Clear();
            CurrentBoard?.GeneratePositions();
        }

        private void OnDrawGizmos()
        {
            foreach (var i in data.GetHexPoints())
            {
                var cell = new Vector3Int(i.q, i.r, 0);
                var position = tileMap.CellToWorld(cell);
                Gizmos.DrawWireSphere(position, 0.93f);
            }
        }
    }
}