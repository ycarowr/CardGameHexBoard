using UnityEngine;

[CreateAssetMenu(menuName = "Variables/UiCardSize")]
public class UiCardSize : ScriptableObject
{
    private const string Path = "Variables/UiCardSize";

    [SerializeField, Tooltip("Size of a card on the X axis."), Range(0, 3)]
    private float x;

    [SerializeField, Tooltip("Size of a card on the Y axis."), Range(0, 3)]
    private float y;

    public Vector2 Value => new Vector2(x, y);

    public static UiCardSize Load()
    {
        return Resources.Load<UiCardSize>(Path);
    }
}