using System;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(DaxDungeonGenerator))]
public class mainDungeonReferences : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DaxDungeonGenerator daxDungeonGenerator = (DaxDungeonGenerator) target;
        
        GUILayout.Space(30f);
        if (GUILayout.Button("Generate Dungeon"))
        {
            daxDungeonGenerator.GenerateDungeon();
        }
        if (GUILayout.Button("Clear Dungeon"))
        {
            daxDungeonGenerator.MainTilemap.ClearAllTiles();
            DestroyImmediate(daxDungeonGenerator.PatronOfSelectedLayoutGO);
        }
    }
}
