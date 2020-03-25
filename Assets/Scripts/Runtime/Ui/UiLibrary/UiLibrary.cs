using Tools.Input.Mouse;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HexCardGame.UI
{
    public class UiLibrary : MonoBehaviour
    {
        private IMouseInput Input { get; set; }
        private UiHoverParticleSystem Hover { get; set; }

        private void Awake()
        {
            Input = GetComponentInChildren<IMouseInput>();
            Hover = GetComponentInChildren<UiHoverParticleSystem>();
            Input.OnPointerEnter += ShowParticlesHover;
            Input.OnPointerExit += HideParticlesHover;
        }


        private void OnDestroy()
        {
            if (Input == null)
                return;
            Input.OnPointerEnter -= ShowParticlesHover;
            Input.OnPointerExit -= HideParticlesHover;
        }

        private void ShowParticlesHover(PointerEventData obj)
        {
            Hover.Show();
        }

        private void HideParticlesHover(PointerEventData obj)
        {
            Hover.Hide();
        }
    }
}