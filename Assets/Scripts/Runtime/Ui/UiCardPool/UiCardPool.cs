using HexCardGame.SharedData;
using Tools.Input.Mouse;
using Tools.UiTransform;
using UnityEngine;

namespace HexCardGame.UI
{
    public interface IUiCardPool : IUiMotionHandler
    {
        ICardData Data { get; }
        void SetColor(Color color);
        void SetAndUpdateView(ICardData data);
        void MoveTo(Vector3 position, float speed, int layer);
    }

    public class UiCardPool : MonoBehaviour, IUiCardPool
    {
        private const int LayerToRenderNormal = 0;
        private const int LayerToRenderTop = 1;
        [SerializeField] private SpriteRenderer artwork;
        [SerializeField] private UiPoolParameters parameters;

        private SpriteRenderer[] Renderers { get; set; }
        private IMouseInput Input { get; set; }
        public ICardData Data { get; private set; }
        public UiMotion Motion { get; private set; }
        public MonoBehaviour MonoBehaviour => this;

        public void SetColor(Color color)
        {
            foreach (var i in Renderers)
                i.color = color;
        }

        public void MoveTo(Vector3 position, float speed, int layer)
        {
            MakeRenderFirst();
            Motion.MoveToWithZ(position, speed, layer);
            Motion.Movement.OnFinishMotion += OnComplete;

            void OnComplete()
            {
                Motion.Movement.OnFinishMotion -= OnComplete;
                MakeRenderNormal();
            }
        }

        public void SetAndUpdateView(ICardData data)
        {
            Data = data;
            UpdateUi();
        }

        private void Awake()
        {
            Renderers = GetComponentsInChildren<SpriteRenderer>();
            GetComponent<BoxCollider2D>().size = parameters.UiCardSize.Value;
            Motion = new UiMotion(this);
        }

        private void Update()
        {
            Motion.Update();
        }

        private void UpdateUi()
        {
            artwork.sprite = Data.Artwork;
        }

        private void MakeRenderFirst()
        {
            for (var i = 0; i < Renderers.Length; i++)
                Renderers[i].sortingOrder = LayerToRenderTop;
        }

        private void MakeRenderNormal()
        {
            for (var i = 0; i < Renderers.Length; i++)
                if (Renderers[i])
                    Renderers[i].sortingOrder = LayerToRenderNormal;
        }
    }
}