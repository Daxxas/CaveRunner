using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRoom : MonoBehaviour
{
    public TileRoomType tileRoomType;
    private Grid grid;
    private bool errorCatched;
    
    void Start()
    {
        grid = GetComponent<Grid>();
        errorCatched = false;
    }
    
    private void OnValidate()
    {
        Start();
    }

    public String GetRoomType()
    {
        return tileRoomType.name;
    }

    public Vector3Int GetStartPoints()
    {
        return tileRoomType.startPosition;
    }
    
    public TileBase[] GetGroundTiles()
    {
        tileRoomType.RoomBounds.z = 0;
        tileRoomType.RoomBounds.size = new Vector3Int(tileRoomType.RoomBounds.size.x, tileRoomType.RoomBounds.size.y, 1);
        Tilemap tilemap = transform.Find("GroundTilemap").gameObject.GetComponent<Tilemap>();
        
        return tilemap.GetTilesBlock(tileRoomType.RoomBounds);
    }

    public BoundsInt GetBounds()
    {
        return tileRoomType.RoomBounds;
    }

    public void setTileRoomType(TileRoomType tileRoomType)
    {
        this.tileRoomType = tileRoomType;
    }

    private void OnDrawGizmos()
    {
        try
        {
            Gizmos.color = new Color(0, 255, 255, 0.5f);
            Gizmos.DrawCube(tileRoomType.RoomBounds.center, tileRoomType.RoomBounds.size);

            Gizmos.color = new Color(255, 0, 0, 0.5f);

            for (int i = 0; i < tileRoomType.door.Length; i++)
            {
                Gizmos.DrawCube(grid.WorldToCell(tileRoomType.door[i]), grid.cellSize);
            }
            
            Gizmos.color = new Color(255, 0, 255, 0.5f);
            
            Gizmos.DrawCube(tileRoomType.startPosition, grid.cellSize);
            
            
        }
        catch(Exception e)
        {
            if (!errorCatched && name != "TypeRoomBase")
            {
                print("Tile Room Type non défini pour : " + name + " à " + AssetDatabase.GetAssetPath(this));
                errorCatched = true;
            }
        }
        
    }
}
