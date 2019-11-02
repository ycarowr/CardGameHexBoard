using HexCardGame.SharedData;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardData), true)]
public class CardDataEditor : Editor
{
    CardData MyTarget => target as CardData;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Space();

        Label("Artwork Preview ");
        GUILayout.FlexibleSpace();
        MyTarget.Artwork = (Sprite) EditorGUILayout.ObjectField(MyTarget.Artwork, typeof(Sprite),
            true,
            GUILayout.Width(100),
            GUILayout.Height(100));

        Space();

        if (GUILayout.Button("Clean Refs"))
            CleanRefs();
    }

    void CleanRefs() => MyTarget.Artwork = null;

    void Space() => GUILayout.Space(25);

    void Bh() => EditorGUILayout.BeginHorizontal();

    void Eh() => EditorGUILayout.EndHorizontal();

    void Bv() => EditorGUILayout.BeginVertical();

    void Ev() => EditorGUILayout.EndVertical();

    void Label(string text, bool isBold = false)
    {
        Bv();
        if (isBold)
            GUILayout.Label(text, EditorStyles.boldLabel, GUILayout.Width(100));
        else
            GUILayout.Label(text, GUILayout.Width(100));
        Ev();
    }
}