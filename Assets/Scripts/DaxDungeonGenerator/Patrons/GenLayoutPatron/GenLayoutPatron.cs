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
    
    private Grid mainGrid;

    public List<RoomPatron> roomPatrons;
    public List<GenLayoutPatron> subLayouts;


    public void Awake()
    {
        genLayouts = Resources.LoadAll<GameObject>("GenLayers/" + genLayoutType.getLayer() + "/" + genLayoutType.name).ToList();
        mainGrid = GetComponentInParent<Grid>();
    }
    private void OnValidate()
    {
        Awake();
    }
    
    
    public void GenerateLayout() //
    {
        int rand = Random.Range(0, genLayouts.Count);
        
        for(int i = 0; i < transform.childCount; i++) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        
        foreach (GameObject room in genLayouts[rand].GetComponent<GenLayout>().GetRooms())
        {
            RoomPatron thisRoom = Instantiate(room, transform).GetComponent<RoomPatron>();
            thisRoom.Awake();
            thisRoom.PickRandomRoom();
            roomPatrons.Add(thisRoom);
        }
        
        foreach (GameObject layout in genLayouts[rand].GetComponent<GenLayout>().GetLayouts())
        {
            GenLayoutPatron thisLayout = Instantiate(layout, transform).GetComponent<GenLayoutPatron>();
            thisLayout.Awake();
            thisLayout.GenerateLayout();
            subLayouts.Add(thisLayout);
        }
    }
    
    
    private void OnDrawGizmos()
    {
        try
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
        catch (Exception e)
        {
            Debug.Log("Pas de layout défini pour " + name);
        }
        
    }
}
