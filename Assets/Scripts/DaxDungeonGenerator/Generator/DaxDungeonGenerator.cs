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
    [SerializeField] private GameObject mainLayoutPatronPrefab;
    private GameObject patronOfSelectedLayoutGO;
    
    public Tilemap MainTilemap => mainTilemap;
    public GameObject PatronOfSelectedLayoutGO => patronOfSelectedLayoutGO;
    
    void Start()
    {
        mainTilemap = GetComponentInChildren<TilemapCollider2D>().gameObject.GetComponent<Tilemap>();
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
        layoutPatronOfSelectedLayout.Awake();
        layoutPatronOfSelectedLayout.GenerateLayout(); //On prend le layout sélectionné et on le réveille
        
        
        GenerateRoomPatron(layoutPatronOfSelectedLayout.roomPatrons); //On demande au layout actuel de générer ses salles
        
        foreach (GenLayoutPatron layoutPatron in layoutPatronOfSelectedLayout.subLayouts) //On demande aux sous layouts de générer leurs salles
        {
            //TO DO : Supporter des sous-layout infini, pour l'instant ça ne supporte pas les sous-sous layout
            GenerateRoomPatron(layoutPatron.roomPatrons);
        }
    }

    private void GenerateRoomPatron(List<RoomPatron> roomPatrons)
    {
        foreach (RoomPatron roomPatron in roomPatrons)
        {
            PlaceTileRoom(roomPatron.GetPatronBounds(), roomPatron.GeneratedRoomTiles);
            //récupérer d'autres infos ici
        }
    }

    private void PlaceTileRoom(BoundsInt bounds, TileBase[] tileArray)
    {
        mainTilemap.SetTilesBlock(bounds, tileArray);
    }
}
