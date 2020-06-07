using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileRoomType))]
public class TileRoomTypeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TileRoomType tileRoomType = (TileRoomType) target;
        
        GUILayout.Space(10f);
        if (GUILayout.Button("Generate Directory"))
        {
            tileRoomType.CheckAndCreateDir();
        }
        GUILayout.Space(10f);
        GUILayout.Toggle(AssetDatabase.IsValidFolder("Assets/Resources/Rooms/" + tileRoomType.name), "Has valid directory");

        GUILayout.TextField("Directory : " + "Assets/Resources/Rooms/" + tileRoomType.name);
    }
    
}
