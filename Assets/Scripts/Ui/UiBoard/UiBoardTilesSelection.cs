using Game.Ui;
using HexBoardGame.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace HexCardGame.UI
{
    public class UiBoardTilesSelection : UiEventListener, IOnRightClickTile
    {
        [SerializeField] UiBoardHightlight boardHighlight;
        [SerializeField] GameObject content;
        [SerializeField] Button hideButton;
        [SerializeField] RectTransform menu;
        [SerializeField] Button neighboursButton;
        [SerializeField] Button diagonalAscButton;
        [SerializeField] Button diagonalDesButton;

        Vector3Int Selection { get; set; }

        void IOnRightClickTile.OnRightClickTile(Vector3Int position, Vector2 screenPosition)
        {
            var rect = menu.rect;
            var offsetX = rect.size.x / 2;
            var offsetY = -rect.size.y / 2;
            menu.anchoredPosition = screenPosition + new Vector2(offsetX, offsetY);
            Selection = position;
            Show();
        }

        protected override void Awake()
        {
            base.Awake();
            hideButton.onClick.AddListener(Hide);
            neighboursButton.onClick.AddListener(OnPressNeighbours);
            diagonalAscButton.onClick.AddListener(OnPressDiagonalAsc);
            diagonalDesButton.onClick.AddListener(OnPressDiagonalDes);
            Hide();
        }

        void OnPressDiagonalDes()
        {
            var selection = GameData.CurrentGameInstance.BoardManipulation.GetDiagonalDescendant(Selection, 10);
            boardHighlight.Show(selection);
            Hide();
        }

        void OnPressNeighbours()
        {
            var selection = GameData.CurrentGameInstance.BoardManipulation.GetNeighbours(Selection);
            boardHighlight.Show(selection);
            Hide();
        }
        
        void OnPressDiagonalAsc()
        {
            
            var selection = GameData.CurrentGameInstance.BoardManipulation.GetDiagonalAscendant(Selection, 10);
            boardHighlight.Show(selection);
            Hide();
        }


        void Show() => content.SetActive(true);
        void Hide() => content.SetActive(false);
    }
}