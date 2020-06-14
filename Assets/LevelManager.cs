using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Controller2D))]
public class LevelManager : MonoBehaviour
{
    public DaxDungeonGenerator daxDungeonGenerator;
    private LevelLoader levelLoader;
    [SerializeField] private GameObject looseDisplay;
    [SerializeField] private TextMeshProUGUI looseText;
    
    // Start is called before the first frame update
    void Start()
    {
        looseDisplay.SetActive(false);
        levelLoader = GameObject.FindObjectOfType<LevelLoader>();
        print("ME DECOMMENTER A LA FIN");
        //daxDungeonGenerator.GenerateDungeon();
    }

    public void DungeonFinished()
    {
        levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void DungeonLoose()
    {
        levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex - 1);
        FindObjectOfType<CoinHUD>().ResetScore();
    }

    public void SetLooseText(String input)
    {
        looseText.SetText(input);
    }
    
    public void DisplayLooseScreen()
    {
        looseDisplay.SetActive(true);
    }
}
