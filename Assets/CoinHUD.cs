using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinHUD : MonoBehaviour
{
    private int score = 0;

    public int Score
    {
        get => score;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            gameObject.SetActive(false);
        }
    }
    
    public void ResetScore()
    {
        score = 0;
        GetComponentInChildren<TextMeshProUGUI>().SetText("x" + score);
    }
    
    public void AddScore(int i)
    {
        score += i;
        GetComponentInChildren<TextMeshProUGUI>().SetText("x" + score);
        FindObjectOfType<LevelManager>().SetLooseText("Score - " + score);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
