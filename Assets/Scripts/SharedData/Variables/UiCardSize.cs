using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using UnityEngine;


[CreateAssetMenu (menuName = "Variables/UiCardSize")]
public class UiCardSize : ScriptableObject
{
    const string Path = "Variables/UiCardSize";
    public static UiCardSize Load() => Resources.Load<UiCardSize>(Path);
    [SerializeField] [Tooltip("Size of a card on the X axis.")] [Range(0, 3)]float x;
    [SerializeField] [Tooltip("Size of a card on the Y axis.")] [Range(0, 3)]float y;
    public Vector2 Value => new Vector2(x, y);
}
