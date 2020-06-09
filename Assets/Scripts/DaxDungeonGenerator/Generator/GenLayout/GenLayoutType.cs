using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New GenLayout", menuName = "GenLayout Type")]
public class GenLayoutType : ScriptableObject
{
    public BoundsInt layoutBounds;
    public Vector3Int[] door;
    public String layer;
    public String childLayer;
    
    public String getLayer()
    {
        return layer;
    }
    
    public void CheckAndCreateDir()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources/GenLayers/" + layer))
        {
            AssetDatabase.CreateFolder("Assets/Resources/GenLayers", layer);
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/GenLayers/" + layer + "/" + this.name))
        {
            AssetDatabase.CreateFolder("Assets/Resources/GenLayers/" + layer, this.name);
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/GenLayoutTypes/" + layer))
        {
            AssetDatabase.CreateFolder("Assets/Resources/GenLayoutTypes", layer);
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/GenLayoutTypes"))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "GenLayoutTypes");
        }
    }
    
    public static GameObject GetBaseGenLayout()
    {
        return Resources.Load<GameObject>("GenLayoutBase/GenLayoutBase");
    }
}

