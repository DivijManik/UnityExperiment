using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UiHelperScript))]
public class UiEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        UiHelperScript s = (UiHelperScript)target;

        GUILayout.Space(5);

        if (GUILayout.Button("Build Object", GUILayout.Height(50)))
        {
            s.InstantiateUI();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("Undo"))
        {
            s.DestroyLast();
        }
    }
}


