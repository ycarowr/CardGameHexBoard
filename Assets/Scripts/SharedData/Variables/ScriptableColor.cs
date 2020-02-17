using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Variables/Color")]
public class ScriptableColor : ScriptableObject
{
    [SerializeField] Color value = Color.white;
    public Color Value => value;
}
