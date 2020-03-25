using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Color")]
public class ScriptableColor : ScriptableObject
{
    [SerializeField] private Color value = Color.white;
    public Color Value => value;
}