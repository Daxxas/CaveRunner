using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

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
            daxDungeonGenerator.ResetDungeon();
            daxDungeonGenerator.GenerateDungeon();
        }
        if (GUILayout.Button("Clear Dungeon"))
        {
            daxDungeonGenerator.ResetDungeon();
        }
        
        GUILayout.Space(20);

        if (GUILayout.Button("Generate Debug Type Room"))
        {
            daxDungeonGenerator.ResetDungeon();
            List<GameObject> tileRoomsOfType = Resources.LoadAll<GameObject>("Rooms/" + daxDungeonGenerator.debugTileRoomType.name).ToList();

            int rand = Random.Range(0, tileRoomsOfType.Count);

            daxDungeonGenerator.GenerateTileRoom(Vector3.zero, tileRoomsOfType[rand].GetComponent<TileRoom>());

            Resources.UnloadUnusedAssets();
        }

        if (GUILayout.Button("Generate Debug Room"))
        {
            daxDungeonGenerator.ResetDungeon();
            TileRoom debugTileRoom = daxDungeonGenerator.debugTileRoomGO.GetComponent<TileRoom>();
            BoundsInt debugBounds = new BoundsInt(Vector3Int.zero, debugTileRoom.tileRoomType.RoomBounds.size);
            daxDungeonGenerator.GenerateTileRoom(Vector3.zero, debugTileRoom);
        }
        
    }
}
