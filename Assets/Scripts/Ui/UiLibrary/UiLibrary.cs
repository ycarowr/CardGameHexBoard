using Tools.Input.Mouse;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HexCardGame.UI
{
    public class UiLibrary : MonoBehaviour
    {
        IMouseInput Input { get; set; }
        UiHoverParticleSystem Hover { get; set; }

        void Awake()
        {
            Input = GetComponentInChildren<IMouseInput>();
            Hover = GetComponentInChildren<UiHoverParticleSystem>();
            Input.OnPointerEnter += ShowParticlesHover;
            Input.OnPointerExit += HideParticlesHover;
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