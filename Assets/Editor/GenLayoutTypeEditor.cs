using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GenLayoutType))]
public class GenLayoutTypeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GenLayoutType genLayoutType = (GenLayoutType) target;
        
        GUILayout.Space(10f);
        if (GUILayout.Button("Generate Directory"))
        {
            genLayoutType.CheckAndCreateDir();
            
        }
        GUILayout.Space(10f);
        GUILayout.Toggle(AssetDatabase.IsValidFolder("Assets/Resources/GenLayers/" + genLayoutType.layer + "/" + genLayoutType.name), "Has valid directory");

        GUILayout.TextField("Directory : " + "Assets/Resources/GenLayers/" + genLayoutType.layer + "/" + genLayoutType.name);
    }
}
