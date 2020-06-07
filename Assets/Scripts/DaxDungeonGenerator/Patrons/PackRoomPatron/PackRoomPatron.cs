using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PackRoomPatron : MonoBehaviour
{

    private List<GameObject> packRooms;

    public PackRoomType patronPackType;
    
    private GameObject mainMapGO;
    private Grid mainGrid;
    private DaxDungeonGenerator dungeonGenerator;
    
    void Awake()
    {
        packRooms = Resources.LoadAll<GameObject>("PackRooms/" + patronPackType.name).ToList();
        
        mainMapGO = GameObject.FindWithTag("MainGrid").gameObject;
        mainGrid = mainMapGO.GetComponent<Grid>();
        dungeonGenerator = mainMapGO.transform.GetChild(0).GetComponent<DaxDungeonGenerator>();
    }

    private void OnValidate()
    {
        Awake();
    }
    
    
    public void PlacePackRoom()
    {
        int rand = Random.Range(0, packRooms.Count);
        
        for(int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        foreach (GameObject room in packRooms[rand].GetComponent<PackRoom>().GetRooms())
        {
            Instantiate(room, transform).GetComponent<RoomPatron>().Awake();
        }
    }

    public void GeneratePackRoom()
    {
        for(int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).GetComponent<RoomPatron>().GenerateRoom();
        }
    }

    public void PlaceAndGeneratePackRoom()
    {
        int rand = Random.Range(0, packRooms.Count);
        
        for(int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        foreach (GameObject room in packRooms[rand].GetComponent<PackRoom>().GetRooms())
        {
            var thisRoom = Instantiate(room, transform).GetComponent<RoomPatron>();
            thisRoom.Awake();
            thisRoom.GenerateRoom();
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255,255,255, 0.1f);
        Gizmos.DrawCube(mainGrid.WorldToCell(transform.position + patronPackType.PackRoomBounds.center), patronPackType.PackRoomBounds.size);

        Gizmos.color = new Color(0,255,0, 1f);
        for (int i = 0; i < patronPackType.door.Length; i++)
        {
            Vector3 worldPosition = patronPackType.door[i] + transform.position;
            
            Gizmos.DrawCube(mainGrid.WorldToCell(worldPosition), mainGrid.cellSize);
        }
    }
}
