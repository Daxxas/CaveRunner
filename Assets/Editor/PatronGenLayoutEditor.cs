using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GenLayoutPatron))]
public class PatronGenLayoutEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GenLayoutPatron genLayoutPatron = (GenLayoutPatron) target;

        if (GUILayout.Button("Generate Layout")) 
        {
            genLayoutPatron.GenerateLayout();
        }
    }
}
