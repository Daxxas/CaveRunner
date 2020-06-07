using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenLayoutPatron : MonoBehaviour
{

    private List<GameObject> genLayouts = new List<GameObject>();
    private List<GameObject> rooms = new List<GameObject>();

    public GenLayoutType genLayoutType;
    
    private GameObject mainMapGO;
    private Grid mainGrid;
    private DaxDungeonGenerator dungeonGenerator;
    
    void Awake()
    {
        genLayouts = Resources.LoadAll<GameObject>(genLayoutType.getLayer() + "/" + genLayoutType.name).ToList();
        
        mainMapGO = GameObject.FindWithTag("MainGrid").gameObject;
        mainGrid = mainMapGO.GetComponent<Grid>();
        dungeonGenerator = mainMapGO.transform.GetChild(0).GetComponent<DaxDungeonGenerator>();
    }

    private void OnValidate()
    {
        Awake();
    }
    
    
    public void GenerateLayout()
    {
        int rand = Random.Range(0, genLayouts.Count);
        
        for(int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        foreach (GameObject room in genLayouts[rand].GetComponent<GenLayout>().GetRooms())
        {
            var thisRoom = Instantiate(room, transform).GetComponent<RoomPatron>();
            thisRoom.Awake();
            thisRoom.GenerateRoom();
        }
        
        foreach (GameObject layout in genLayouts[rand].GetComponent<GenLayout>().GetLayouts())
        {
            var thisLayout = Instantiate(layout, transform).GetComponent<GenLayoutPatron>();
            thisLayout.Awake();
            thisLayout.GenerateLayout();
        }
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255,255,255, 0.1f);
        Gizmos.DrawCube(mainGrid.WorldToCell(transform.position + genLayoutType.layoutBounds.center), genLayoutType.layoutBounds.size);

        Gizmos.color = new Color(0,255,0, 1f);
        for (int i = 0; i < genLayoutType.door.Length; i++)
        {
            Vector3 worldPosition = genLayoutType.door[i] + transform.position;
            
            Gizmos.DrawCube(mainGrid.WorldToCell(worldPosition), mainGrid.cellSize);
        }
    }
}
