using HexCardGame.SharedData;
using Tools.UiTransform;
using UnityEngine;

namespace HexCardGame.UI
{
    public interface IUiCardPool : IUiMotionHandler
    {
        ICardData Data { get; }
        void SetColor(Color color);
        void SetAndUpdateView(ICardData data);
    }

    public class UiCardPool : MonoBehaviour, IUiCardPool
    {
        [SerializeField] SpriteRenderer artwork;
        [SerializeField] UiPoolParameters parameters;

        SpriteRenderer[] Renderers { get; set; }
        public ICardData Data { get; private set; }
        public UiMotion Motion { get; private set; }
        public MonoBehaviour MonoBehaviour => this;

        public void SetColor(Color color)
        {
            foreach (var i in Renderers)
                i.color = color;
        }

        public void SetAndUpdateView(ICardData data)
        {
            Data = data;
            UpdateUi();
        }

        void Awake()
        {
            Renderers = GetComponentsInChildren<SpriteRenderer>();
            GetComponent<BoxCollider2D>().size = parameters.UiCardSize.Value;
            Motion = new UiMotion(this);
        }

        void Update() => Motion.Update();

        void UpdateUi() => artwork.sprite = Data.Artwork;
    }
}