using System.Linq;
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
        [SerializeField] BoardData data;
        GameObject[] positions;
        [SerializeField] GameObject textPosition;
        [SerializeField] Tilemap tileMap;
        IBoard<CreatureElement> CurrentBoard { get; set; }

        void ICreateBoard<CreatureElement>.OnCreateBoard(IBoard<CreatureElement> board)
        {
            CurrentBoard = board;
            DrawPositions();
        }

        [Button]
        void DrawPositions()
        {
            const string uiPosition = "UiPosition_";
            var identity = Quaternion.identity;
            ClearPositions();
            positions = new GameObject[CurrentBoard.Positions.Length];
            for (var i = 0; i < CurrentBoard.Positions.Length; i++)
            {
                var p = CurrentBoard.Positions[i];
                var worldPosition = tileMap.CellToWorld(p);
                var gameObj = Instantiate(textPosition, worldPosition, identity, transform);
                positions[i] = gameObj;
                var tmpText = gameObj.GetComponent<TMP_Text>();
                var sPosition = $"x:{p.x}\ny:{p.y}";
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
            tileMap.ClearAllTiles();
            CurrentBoard?.Clear();
            CurrentBoard?.GeneratePositions();
        }

        void OnDrawGizmos()
        {
            foreach (var i in data.GetHexPositions())
            {
                var cell = new Vector3Int(i.x, i.y, 0);
                var position = tileMap.CellToWorld(cell);
                Gizmos.DrawWireSphere(position, 0.93f);
            }
        }
    }
}