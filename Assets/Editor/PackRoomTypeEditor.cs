using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PackRoomType))]
public class PackRoomTypeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PackRoomType tileRoomType = (PackRoomType) target;
        
        GUILayout.Space(10f);
        if (GUILayout.Button("Generate Directory"))
        {
            tileRoomType.CheckAndCreateDir();
        }
        GUILayout.Space(10f);
        GUILayout.Toggle(AssetDatabase.IsValidFolder("Assets/Resources/PackRooms/" + tileRoomType.name), "Has valid directory");

        GUILayout.TextField("Directory : " + "Assets/Resources/PackRooms/" + tileRoomType.name);
    }
}
