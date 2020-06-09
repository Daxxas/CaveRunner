using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomPatron))]
public class PatronEditor : Editor
{
    private bool showTypeEditor = true;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RoomPatron roomPatron = (RoomPatron) target;

        if (GUILayout.Button("Generate Room"))
        {
            roomPatron.PickRandomRoom();
        }
        
        GUILayout.Space(20f);
        Editor typeEditor = CreateEditor(roomPatron.patronRoomType);
        
        String typeHeader = roomPatron.patronRoomType.name;

        showTypeEditor = EditorGUILayout.Foldout (showTypeEditor, typeHeader, true);
        ;
        if (showTypeEditor)
        {
            typeEditor.OnInspectorGUI();
        }
    }
}
