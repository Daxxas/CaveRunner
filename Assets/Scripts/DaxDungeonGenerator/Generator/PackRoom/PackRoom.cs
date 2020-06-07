using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Grid))]
public class PackRoom : MonoBehaviour
{
    public PackRoomType PackType;
    
    private Grid grid;
    
    void Start()
    {
        grid = GetComponent<Grid>();
    }

    private void OnValidate()
    {
        Start();
    }

    public List<GameObject> GetRooms()
    {
        List<GameObject> childList = new List<GameObject>(); 
        foreach (Transform child in transform)
        {
            childList.Add(child.gameObject);
        }

        return childList;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255,255,255, 0.2f);
        Gizmos.DrawCube(grid.WorldToCell(PackType.PackRoomBounds.center), PackType.PackRoomBounds.size);

        Gizmos.color = new Color(0,255,0, 1f);

        for (int i = 0; i < PackType.door.Length; i++)
        {
            Gizmos.DrawCube(grid.WorldToCell(PackType.door[i]), grid.cellSize);
        }
    }
}
