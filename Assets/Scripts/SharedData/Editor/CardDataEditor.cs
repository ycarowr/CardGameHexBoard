using HexCardGame.SharedData;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardData), true)]
public class CardDataEditor : Editor
{
    private CardData MyTarget => target as CardData;

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

    private void CleanRefs()
    {
        MyTarget.Artwork = null;
    }

    private void Space()
    {
        GUILayout.Space(25);
    }

    private void Bh()
    {
        EditorGUILayout.BeginHorizontal();
    }

    private void Eh()
    {
        EditorGUILayout.EndHorizontal();
    }

    private void Bv()
    {
        EditorGUILayout.BeginVertical();
    }

    private void Ev()
    {
        EditorGUILayout.EndVertical();
    }

    private void Label(string text, bool isBold = false)
    {
        Bv();
        if (isBold)
            GUILayout.Label(text, EditorStyles.boldLabel, GUILayout.Width(100));
        else
            GUILayout.Label(text, GUILayout.Width(100));
        Ev();
    }
}