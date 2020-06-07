using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Layout", menuName = "Layout")]
public class GenLayoutType : ScriptableObject
{
    //public BoundsInt RoomBounds;

    public void CheckAndCreateDir()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Layouts"))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Layouts");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Layouts" + this.name))
        {
            AssetDatabase.CreateFolder("Assets/Resources/Layouts", this.name);
        }
    }
}
