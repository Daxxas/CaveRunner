using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class RoomPatron : MonoBehaviour
{
    public TileRoomType patronRoomType;
    
    private List<GameObject> tileRooms = new List<GameObject>();
    private List<Vector3Int> generatedRoomDoors = new List<Vector3Int>();
    private bool generatedHasStartposition;
    private Vector3Int generatedStartposition;
    private TileBase[] generatedRoomTiles;
    private BoundsInt generatedBoundsInt;

    public List<Vector3Int> GeneratedRoomDoors => generatedRoomDoors;
    public bool GeneratedHasStartposition => generatedHasStartposition;
    public Vector3Int GeneratedStartposition => generatedStartposition;
    public TileBase[] GeneratedRoomTiles => generatedRoomTiles;
    
    
    public Grid mainGrid;

    public void Awake()
    {
        tileRooms = Resources.LoadAll<GameObject>("Rooms/" + patronRoomType.name).ToList();
        
        mainGrid = GetComponentInParent<Grid>();
    }

    
    private void OnValidate()
    {
        Awake();
    }

    public void PickRandomRoom()
    {
        int rand = Random.Range(0, tileRooms.Count);

        if (tileRooms.Count > 0)
        {
            TileRoom room = tileRooms[rand].GetComponent<TileRoom>();
            SetPatronInfosFromRoomInfos(room);
        }
        else
        {
            print("No room found for room type " + patronRoomType.name);
        }
    }
    
    private void SetPatronInfosFromRoomInfos(TileRoom room)
    {
        generatedRoomDoors = room.tileRoomType.door.ToList();
        generatedStartposition = room.tileRoomType.startPosition;
        generatedHasStartposition = room.tileRoomType.hasStartposition;
        generatedRoomTiles = room.GetGroundTiles();
    }
    
    
    private void OnDrawGizmos()
    {
        
        Gizmos.color = new Color(0,255,255, 0.5f);
        
        Gizmos.DrawCube(mainGrid.WorldToCell(transform.position + patronRoomType.RoomBounds.center), patronRoomType.RoomBounds.size);

        Gizmos.color = new Color(255,0,0, 0.5f);
        for (int i = 0; i < patronRoomType.door.Length; i++)
        {
            Vector3 worldPosition = patronRoomType.door[i] + transform.position;
            
            Gizmos.DrawCube(mainGrid.WorldToCell(worldPosition), mainGrid.cellSize);
        }
    }

    public BoundsInt GetPatronBounds()
    {
        return new BoundsInt(mainGrid.WorldToCell(transform.position), patronRoomType.RoomBounds.size);
    }
}
