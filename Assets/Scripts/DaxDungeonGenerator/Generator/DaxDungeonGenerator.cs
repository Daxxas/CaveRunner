using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class DaxDungeonGenerator : MonoBehaviour
{
    private Tilemap mainTilemap;
    
    [SerializeField] private String finalLayer;
    public GameObject mainLayoutPatronPrefab;
    private GameObject patronOfSelectedLayoutGO;
    private Grid mainGrid;
    public int expandOffset = 16;
    public TileBase groundTile;
    public GameObject mainCharacter;
    
    [Header("Debug")] 
    public TileRoomType debugTileRoomType;
    public GameObject debugTileRoomGO;

    public Tilemap MainTilemap => mainTilemap;
    public GameObject PatronOfSelectedLayoutGO => patronOfSelectedLayoutGO;
    
    void Start()
    {
        mainTilemap = GetComponentInChildren<TilemapCollider2D>().gameObject.GetComponent<Tilemap>();
        mainGrid = GetComponent<Grid>();
        mainCharacter = GameObject.FindWithTag("Player");
    }

    private void OnValidate()
    {
        Start();
    }

    public void GenerateDungeon()
    {
        List<GameObject> genLayouts = Resources.LoadAll<GameObject>("GenLayers/"  + finalLayer).ToList(); //On charge les layouts de la couche voulu
        
        int rand = Random.Range(0, genLayouts.Count);
        patronOfSelectedLayoutGO  = Instantiate(mainLayoutPatronPrefab, transform);
        GenLayoutPatron layoutPatronOfSelectedLayout = patronOfSelectedLayoutGO.GetComponent<GenLayoutPatron>();
        layoutPatronOfSelectedLayout.genLayoutType = genLayouts[rand].GetComponent<GenLayout>().genLayoutType;
        layoutPatronOfSelectedLayout.Awake(); //On prend le layout sélectionné et on le réveille

        BoundsInt expandedLayout = ExpandLayout(layoutPatronOfSelectedLayout.genLayoutType.layoutBounds);

        mainTilemap.SetTile(new Vector3Int(expandedLayout.xMax, expandedLayout.yMax, expandedLayout.z), groundTile); //On place une tile dans le coin sinon la box ne fill pas correctement
        mainTilemap.BoxFill(expandedLayout.position, groundTile, expandedLayout.xMin, expandedLayout.yMin, 200, 200);
        CascadeGenerateLayout(layoutPatronOfSelectedLayout);

        mainCharacter.transform.position = GetComponentInChildren<StartPosition>().transform.position;
    }

    private void CascadeGenerateLayout(GenLayoutPatron genLayoutPatron)
    {
        genLayoutPatron.GenerateLayout();
        GenerateRoomPatron(genLayoutPatron.roomPatrons); 
        foreach (GenLayoutPatron layoutPatron in genLayoutPatron.subLayouts)
        {
            GenerateRoomPatron(layoutPatron.roomPatrons);
            
            if (layoutPatron.subLayouts.Count > 0)
            {
                CascadeGenerateLayout(layoutPatron);
            }
        }
    }

    public void GenerateRoomPatron(List<RoomPatron> roomPatrons)
    {
        foreach (RoomPatron roomPatron in roomPatrons)
        {
            GenerateTileRoom(roomPatron.transform.position, roomPatron.GeneratedTileRoom);
        }
    }

    public void GenerateTileRoom(Vector3 pos, TileRoom tileRoom)
    {
        PlaceTileRoom(PosToCellPos(pos), tileRoom);

        foreach (MonsterSpawner monsterSpawner in tileRoom.GetMonsters())
        {
            PlaceMonsterSpawner(monsterSpawner, pos);
        }

        if (tileRoom.GetExitDoor() != null)
        {
            Instantiate(tileRoom.GetExitDoor(), pos + tileRoom.GetExitDoor().transform.position, transform.rotation,  transform);
        }
        
        if (tileRoom.GetStartPosition() != null)
        {
            Instantiate(tileRoom.GetStartPosition(), pos + tileRoom.GetStartPosition().transform.position, transform.rotation,  transform);
        }

        //Générer les autres choses ici
    }

    private void PlaceTileRoom(Vector3Int pos, TileRoom tileRoom)
    {
        BoundsInt patronBounds = new BoundsInt(pos, tileRoom.tileRoomType.RoomBounds.size);
        mainTilemap.SetTilesBlock(patronBounds, tileRoom.GetGroundTiles());
    }

    private void PlaceMonsterSpawner(MonsterSpawner monsterSpawner, Vector3 patronPos)
    {
        Instantiate(monsterSpawner,patronPos + monsterSpawner.Position, transform.rotation, transform);
    }

    public Vector3Int PosToCellPos(Vector3 pos)
    {
        return mainGrid.WorldToCell(pos);
    }

    private BoundsInt ExpandLayout(BoundsInt layoutBounds)
    {
        BoundsInt expandedBounds = new BoundsInt(new Vector3Int(layoutBounds.x, layoutBounds.y, layoutBounds.z), layoutBounds.size);

        expandedBounds.xMin -= expandOffset;
        expandedBounds.yMin -= expandOffset;
        expandedBounds.xMax += expandOffset;
        expandedBounds.yMax += expandOffset;
        return expandedBounds;

    }
    
    public void ResetDungeon()
    {
        MainTilemap.ClearAllTiles();
        DestroyImmediate(PatronOfSelectedLayoutGO);
        foreach (GenLayoutPatron genLayoutPatron in GetComponentsInChildren<GenLayoutPatron>())
        {
            DestroyImmediate(genLayoutPatron.gameObject);
        }
            
        foreach (RoomPatron roomPatron in GetComponentsInChildren<RoomPatron>())
        {
            DestroyImmediate(roomPatron.gameObject);
        }
            
        foreach (MonsterSpawner monsterSpawner in GetComponentsInChildren<MonsterSpawner>())
        {
            DestroyImmediate(monsterSpawner.gameObject);
        }

        foreach (ExitDoor exitDoor in GetComponentsInChildren<ExitDoor>())
        {
            DestroyImmediate(exitDoor.gameObject);
        }
            
        foreach (StartPosition startPosition in GetComponentsInChildren<StartPosition>())
        {
            DestroyImmediate(startPosition.gameObject);
        }
    }
    
}
