using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof (Grid))]
public class GenLayout : MonoBehaviour
{
    public GenLayoutType genLayoutType;
    
    private Grid grid;
    
    void Start()
    {
        //TO DO : Gérer les grids lorsqu'un layout est dans un layout
        grid = GetComponent<Grid>();
    }

    private void OnValidate()
    {
        Start();
    }

    public List<GameObject> GetRooms()
    {
        List<GameObject> rooms = new List<GameObject>();
        foreach (RoomPatron room in GetComponentsInChildren<RoomPatron>().Where(x => x.gameObject.transform.parent != transform.parent).ToList())
        {
            rooms.Add(room.gameObject);
        }
        return rooms;
    }

    public List<GameObject> GetLayouts()
    {
        List<GameObject> layouts = new List<GameObject>();
        foreach (GenLayoutPatron layout in GetComponentsInChildren<GenLayoutPatron>().ToList().Where(x => x.gameObject.transform.parent != transform.parent))
        {
            layouts.Add(layout.gameObject);
        }
        return layouts;
    }

    public void setGenLayoutType(GenLayoutType genlayout)
    {
        this.genLayoutType = genlayout;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255,255,255, 0.2f);
        Gizmos.DrawCube(grid.WorldToCell(genLayoutType.layoutBounds.center), genLayoutType.layoutBounds.size);

        Gizmos.color = new Color(0,255,0, 1f);

        for (int i = 0; i < genLayoutType.door.Length; i++)
        {
            Gizmos.DrawCube(grid.WorldToCell(genLayoutType.door[i]), grid.cellSize);
        }
    }
}
