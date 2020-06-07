using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New PackRoom", menuName = "PackRoom Type")]
public class PackRoomType : ScriptableObject
{
    public BoundsInt PackRoomBounds;
    public Vector3Int[] door;
    
    public void CheckAndCreateDir()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources/PackRooms"))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "PackRooms");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/PackRooms" + this.name))
        {
            AssetDatabase.CreateFolder("Assets/Resources/PackRooms", this.name);
        }
    }
}

