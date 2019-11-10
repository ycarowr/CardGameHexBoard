using Tools.Input.Mouse;
using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Interface for the used UI Components.
    ///     TODO: To split it in smaller interfaces.
    /// </summary>
    public interface IUiCardComponents
    {
        UiCardParameters CardConfigsParameters { get; }
        Camera MainCamera { get; }
        UiCardHandSelector HandSelector { get; }
        SpriteRenderer[] Renderers { get; }
        SpriteRenderer MyRenderer { get; }
        Collider2D Collider { get; }
        Rigidbody2D Rigidbody { get; }
        IMouseInput Input { get; }
        GameObject gameObject { get; }
        Transform transform { get; }
    }
}