using Game.Ui;
using HexCardGame.Runtime.GamePool;
using Tools.Input.Mouse;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HexCardGame.UI
{
    [ExecuteInEditMode, RequireComponent(typeof(IMouseInput))]
    public class UiPoolPosition : UiEventListener, IDataStorage<IUiCardPool>, ILockPosition, IUnlockPosition
    {
        [SerializeField] PositionId id;
        [SerializeField] UiPoolParameters parameters;
        public PositionId Id => id;
        BoxCollider2D Collider { get; set; }
        UiHoverParticleSystem Hover { get; set; }
        IMouseInput Input { get; set; }
        public bool HasData => Data != null;
        public IUiCardPool Data { get; private set; }

        public void SetData(IUiCardPool value)
        {
            Data = value;
            Data?.MoveTo(transform.position, parameters.PoolMovementSpeed, UiPoolPositioning.LayerZ);
        }

        void ILockPosition.OnLockPosition(PositionId positionId)
        {
            if (id == positionId) Data?.SetColor(parameters.Locked);
        }

        void IUnlockPosition.OnUnlockPosition(PositionId positionId)
        {
            if (id == positionId) Data?.SetColor(parameters.Unlocked);
        }

        protected override void Awake()
        {
            base.Awake();
            Hover = GetComponentInChildren<UiHoverParticleSystem>();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerEnter += ShowParticlesHover;
            Input.OnPointerExit += HideParticlesHover;
            Collider = GetComponent<BoxCollider2D>();
            Collider.size = parameters.UiCardSize.Value;
        }

        public void Clear()
        {
            if (HasData)
            {
                ObjectPooler.Instance.Release(Data.MonoBehaviour.gameObject);
                SetData(null);
            }
        }

        void OnDestroy()
        {
            if (Input == null)
                return;
            Input.OnPointerEnter -= ShowParticlesHover;
            Input.OnPointerExit -= HideParticlesHover;
        }

        void ShowParticlesHover(PointerEventData obj) => Hover.Show();

        void HideParticlesHover(PointerEventData obj) => Hover.Hide();
    }
}