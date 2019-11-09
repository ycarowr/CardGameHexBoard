using HexCardGame.SharedData;
using Tools.UiTransform;
using UnityEngine;
using Logger = Tools.Logger;

namespace HexCardGame.UI
{
    public interface IUiCardPool : IUiMotionHandler
    {
        void SetColor(Color color);
    }

    public class UiCardPool : MonoBehaviour, IUiCardPool
    {
        [SerializeField] UiPoolParameters parameters;
        public ICardData Data { get; private set; }

        SpriteRenderer[] Renderers { get; set; }
        public UiMotion Motion { get; private set; }
        public MonoBehaviour MonoBehaviour => this;

        public void SetColor(Color color)
        {
            foreach (var i in Renderers)
                i.color = color;
        }

        void Awake()
        {
            Renderers = GetComponentsInChildren<SpriteRenderer>();
            GetComponent<BoxCollider2D>().size = parameters.UiCardSize.Value;
            Motion = new UiMotion(this);
        }

        void Update() => Motion.Update();

        public void SetData(ICardData data)
        {
            Data = data;
            UpdateUi();
        }

        void UpdateUi() => Logger.Log<UiCardPool>("Ui updated with dataId: " + Data.Id);
    }
}