using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New TileRoom", menuName = "TileRoom Type")]
public class TileRoomType : ScriptableObject
{
    public BoundsInt RoomBounds;
    public Vector3Int[] door;

    public void CheckAndCreateDir()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Rooms"))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Rooms");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Rooms" + this.name))
        {
            AssetDatabase.CreateFolder("Assets/Resources/Rooms", this.name);
        }
    }
}

