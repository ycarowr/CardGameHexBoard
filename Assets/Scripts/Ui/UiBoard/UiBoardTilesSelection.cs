using Game.Ui;
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
            Hide();
        }

        void OnPressNeighbours()
        {
            var selection = GameData.CurrentGameInstance.BoardManipulation.GetNeighbours(Selection.x, Selection.y);
            boardHighlight.Show(selection);
            Hide();
        }

        void Show() => content.SetActive(true);
        void Hide() => content.SetActive(false);
    }
}