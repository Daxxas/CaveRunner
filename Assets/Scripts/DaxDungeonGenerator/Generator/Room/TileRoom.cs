using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRoom : MonoBehaviour
{
    public TileRoomType tileRoomType;
    private Grid grid;
    
    void Start()
    {
        grid = GetComponent<Grid>();
    }
    
    private void OnValidate()
    {
        Start();
    }

    public String GetRoomType()
    {
        return tileRoomType.name;
    }
    
    public TileBase[] GetGroundTiles()
    {
        Tilemap tilemap = transform.Find("GroundTilemap").gameObject.GetComponent<Tilemap>();
        return tilemap.GetTilesBlock(tileRoomType.RoomBounds);
    }

    public BoundsInt GetBounds()
    {
        return tileRoomType.RoomBounds;
    }

    private void OnDrawGizmos()
    {
        Tilemap tilemap = transform.Find("GroundTilemap").gameObject.GetComponent<Tilemap>();
        
        Gizmos.color = new Color(0,255,255, 0.5f);
        Gizmos.DrawCube(tileRoomType.RoomBounds.center, tileRoomType.RoomBounds.size);

        Gizmos.color = new Color(255,0,0, 0.5f);

        for (int i = 0; i < tileRoomType.door.Length; i++)
        {
            Gizmos.DrawCube(grid.WorldToCell(tileRoomType.door[i]), grid.cellSize);
        }
    }
}
