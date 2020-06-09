using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (GenLayout))]
public class GenLayoutEditor : Editor
{
    bool showTypeEditor = true;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(20f);
        GenLayout genLayout = (GenLayout) target;
        Editor typeEditor = CreateEditor(genLayout.genLayoutType);
        
        String typeHeader = genLayout.genLayoutType.name;

        showTypeEditor = EditorGUILayout.Foldout (showTypeEditor, typeHeader, true);
        
        if (showTypeEditor)
        {
            typeEditor.OnInspectorGUI();
        }
    }
}

