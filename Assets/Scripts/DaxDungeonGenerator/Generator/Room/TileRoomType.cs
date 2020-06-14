using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New TileRoom", menuName = "TileRoom Type")]
public class TileRoomType : ScriptableObject
{
    public BoundsInt RoomBounds;
    public Vector3Int[] door;
    public bool hasStartposition;
    public Vector3Int startPosition;

    public void CheckAndCreateDir()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Rooms"))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Rooms");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Rooms" + this.name))
        {
            AssetDatabase.CreateFolder("Assets/Resources/Rooms", this.name);
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/RoomTypes")) 
        {
            AssetDatabase.CreateFolder("Assets/Resources", "RoomTypes");
        }
    }

    public static GameObject GetBaseTileRoom()
    {
        return Resources.Load<GameObject>("TypeRoomBase/TypeRoomBase");
    }
}


