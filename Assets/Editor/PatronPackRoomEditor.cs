using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PackRoomPatron))]
public class PatronPackRoomEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PackRoomPatron packRoomPatron = (PackRoomPatron) target;

        if (GUILayout.Button("Place Rooms")) 
        {
            packRoomPatron.PlacePackRoom();
        }
        if (GUILayout.Button("Generate Placed Rooms")) 
        {
            packRoomPatron.GeneratePackRoom();
        }
        if (GUILayout.Button("Place & Generate Rooms"))
        {
            packRoomPatron.PlaceAndGeneratePackRoom();
        }
    }
}
