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

    private GameObject mainMapGO;
    private Grid mainGrid;
    private DaxDungeonGenerator dungeonGenerator;

    public void Awake()
    {
        tileRooms = Resources.LoadAll<GameObject>("Rooms/" + patronRoomType.name).ToList();
        
        mainMapGO = GameObject.FindWithTag("MainGrid").gameObject;
        mainGrid = mainMapGO.GetComponent<Grid>();
        dungeonGenerator = mainMapGO.transform.GetChild(0).GetComponent<DaxDungeonGenerator>();
    }

    
    private void OnValidate()
    {
        Awake();
    }

    public void GenerateRoom()
    {
        int rand = Random.Range(0, tileRooms.Count);
        
;       dungeonGenerator.placeTileRoom(GetPatronGridPos(), tileRooms[rand].GetComponent<TileRoom>());
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

    private Vector3Int GetPatronGridPos()
    {
        return mainGrid.WorldToCell(transform.position);
    }
}
