using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DaxDungeonGenerator : MonoBehaviour
{
    private List<TileRoom> tileRooms;
    private Tilemap mainTilemap;
    
    public GameObject roomTest;
    private TileRoom roomTileRoom;

    public BoundsInt area;

    void Start()
    {
        roomTileRoom = roomTest.GetComponent<TileRoom>();
        mainTilemap = GetComponent<Tilemap>();
        
        /*
        Tilemap Room1 = tileRooms[0].GetTilemap();
        mainTilemap.SetTilesBlock(Room1.cellBounds, Room1.GetTilesBlock(Room1.cellBounds));

        Tilemap Room2 = tileRooms[1].GetTilemap();
        BoundsInt R2 = Room2.cellBounds;
        R2.position = new Vector3Int(Room2.cellBounds.x*2 + Room1.cellBounds.x, Room2.cellBounds.y, Room2.cellBounds.z);
        mainTilemap.SetTilesBlock(R2, Room2.GetTilesBlock(Room2.cellBounds));*/
    }

    private void OnValidate()
    {
        Start();
    }


    private BoundsInt RoomBoundsToPos(TileRoom room, Vector3Int pos)
    {
        return new BoundsInt(pos, room.GetBounds().size);
    }

    public void placeTileRoom(Vector3Int pos, TileRoom room)
    {
        mainTilemap.SetTilesBlock(RoomBoundsToPos(room, pos), room.GetGroundTiles());
    }

    public void JETEST()
    {
        print("ALLO");
    }
    
    void Update()
    {
        
    }
}
