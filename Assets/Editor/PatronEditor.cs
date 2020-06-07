using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomPatron))]
public class PatronEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RoomPatron roomPatron = (RoomPatron) target;

        if (GUILayout.Button("Generate Room"))
        {
            roomPatron.GenerateRoom();
        }
    }
}
