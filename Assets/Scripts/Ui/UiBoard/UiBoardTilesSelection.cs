using Game.Ui;
using HexBoardGame.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace HexCardGame.UI
{
    public class UiBoardTilesSelection : UiEventListener, IOnRightClickTile
    {
        [SerializeField] Canvas canvas;
        [SerializeField] UiBoardHightlight boardHighlight;
        [SerializeField] GameObject content;
        [SerializeField] Button hideButton;
        [SerializeField] RectTransform menu;
        [SerializeField] Button neighboursButton;
        [SerializeField] Button diagonalAscButton;
        [SerializeField] Button diagonalDesButton;

        Vector3Int Selection { get; set; }

        void IOnRightClickTile.OnRightClickTile(Vector3Int cell, Vector2 screenPoint)
        {
            var referenceResolution = canvas.GetComponent<CanvasScaler>().referenceResolution;
            var currentResolution = new Vector2(Screen.width, Screen.height);
            var factorResolution = currentResolution / referenceResolution;
            var rectSize = menu.rect.size;
            var offsetX = rectSize.x / 2;
            var offsetY = -rectSize.y / 2;
            //TODO (Bug):
            //Demands Full HD resolution or any equivalent aspect ratio (1.77), otherwise the placement is done in a wrong way.
            menu.anchoredPosition = screenPoint / factorResolution + new Vector2(offsetX, offsetY);
            Selection = cell;
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