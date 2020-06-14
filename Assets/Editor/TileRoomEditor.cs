using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CustomEditor(typeof (TileRoom))]
public class TileRoomEditor : Editor
{
    bool showTypeEditor = true;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(20f);
        TileRoom tileRoom = (TileRoom) target;
        Editor typeEditor = CreateEditor(tileRoom.tileRoomType);
        
        String typeHeader = tileRoom.tileRoomType.name;

        showTypeEditor = EditorGUILayout.Foldout (showTypeEditor, typeHeader, true);
        ;
        if (showTypeEditor)
        {
            typeEditor.OnInspectorGUI();
        }
    }
}

