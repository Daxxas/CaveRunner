using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileRoomType))]
public class TileRoomTypeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TileRoomType tileRoomType = (TileRoomType) target;
        
        GameObject baseTypeRoom = Resources.Load<GameObject>("RoomTypes/" + tileRoomType.name);

        String tileRoomTypeRoomsPath = "Assets/Resources/Rooms/" + tileRoomType.name;
        
        GUILayout.Space(10f);
        
        if (GUILayout.Button("Generate New Room"))
        {
            CreateNewRoom();
        }
        
        
        GUILayout.Space(10f);
        
        if (GUILayout.Button("Generate Base Room"))
        {
            CreateGenericRoomForType();
        }
        
        GUILayout.Space(10f);
        
        if (GUILayout.Button("Generate Directory"))
        {
            tileRoomType.CheckAndCreateDir();
        }


        GUILayout.Space(10f);
        GUILayout.Toggle(AssetDatabase.IsValidFolder("Assets/Resources/Rooms/" + tileRoomType.name), "Has valid directory");

        GUILayout.TextField("Directory : " + tileRoomTypeRoomsPath);

        void CreateNewRoom()
        {
            try
            {
                var modelRootGO = baseTypeRoom;
                var instanceRoot = (GameObject) PrefabUtility.InstantiatePrefab(modelRootGO);
                instanceRoot.GetComponent<TileRoom>().setTileRoomType(tileRoomType);
                PrefabUtility.SaveAsPrefabAsset(instanceRoot, tileRoomTypeRoomsPath + "/" + "New " + tileRoomType.name + ".prefab");
                DestroyImmediate(instanceRoot);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        
        void CreateGenericRoomForType()
        {
            var modelRootGO = TileRoomType.GetBaseTileRoom();
            var instanceRoot = (GameObject) PrefabUtility.InstantiatePrefab(modelRootGO);
            instanceRoot.GetComponent<TileRoom>().setTileRoomType(tileRoomType);
            PrefabUtility.SaveAsPrefabAsset(instanceRoot, "Assets/Resources/RoomTypes/" + tileRoomType.name + ".prefab");
            DestroyImmediate(instanceRoot);
        }
    }
}
