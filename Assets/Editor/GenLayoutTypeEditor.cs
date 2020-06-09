using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GenLayoutType))]
public class GenLayoutTypeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GenLayoutType genLayoutType = (GenLayoutType) target;
        
        GameObject baseGenLayout = Resources.Load<GameObject>("GenLayoutTypes/" + genLayoutType.layer + "/" + genLayoutType.name);

        String genLayoutTypePath = "Assets/Resources/GenLayers/" + genLayoutType.layer + "/" + genLayoutType.name;

        GUILayout.Space(10f);
        
        if (GUILayout.Button("Generate New Layout"))
        {
            CreatNewLayout();
        }

        GUILayout.Space(10f);
        
        if (GUILayout.Button("Generate Base Layout"))
        {
            CreateGenericLayoutForType();
        }

        GUILayout.Space(10f);
        if (GUILayout.Button("Generate Directory"))
        {
            genLayoutType.CheckAndCreateDir();
            
        }
        GUILayout.Space(10f);
        GUILayout.Toggle(AssetDatabase.IsValidFolder("Assets/Resources/GenLayers/" + genLayoutType.layer + "/" + genLayoutType.name), "Has GenLayouts Directory");
        GUILayout.TextField("Directory : " + "Assets/Resources/GenLayers/" + genLayoutType.layer + "/" + genLayoutType.name);
        GUILayout.Space(5f);
        GUILayout.Toggle(AssetDatabase.IsValidFolder("Assets/Resources/GenLayoutTypes"), "Has GenLayoutTypes Directory");
        GUILayout.TextField("Directory : " + "Assets/Resources/GenLayoutTypes");
        GUILayout.Space(5f);
        GUILayout.Toggle(AssetDatabase.IsValidFolder("Assets/Resources/GenLayoutTypes/" + genLayoutType.layer), "Has GenLayoutTypes layer subfolder");
        GUILayout.TextField("Directory : " + "Assets/Resources/GenLayoutTypes/" + genLayoutType.layer);

        
        void CreatNewLayout()
        {
            var modelRootGO = baseGenLayout;
            var instanceRoot = (GameObject) PrefabUtility.InstantiatePrefab(modelRootGO);
            instanceRoot.GetComponent<GenLayout>().setGenLayoutType(genLayoutType);
            PrefabUtility.SaveAsPrefabAsset(instanceRoot, genLayoutTypePath + "/" + "New " + genLayoutType.name + ".prefab");
            DestroyImmediate(instanceRoot);
        }
        
        void CreateGenericLayoutForType()
        {
            var modelRootGO = GenLayoutType.GetBaseGenLayout();
            var instanceRoot = (GameObject) PrefabUtility.InstantiatePrefab(modelRootGO);
            instanceRoot.GetComponent<GenLayout>().setGenLayoutType(genLayoutType);
            PrefabUtility.SaveAsPrefabAsset(instanceRoot, "Assets/Resources/GenLayoutTypes/" + genLayoutType.layer + "/" + genLayoutType.name + ".prefab");
            DestroyImmediate(instanceRoot);
        }
    }
}
