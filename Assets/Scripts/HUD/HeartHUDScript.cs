using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HeartHUDScript : MonoBehaviour
{
    public PlayerCombat mainCharacter;
    [SerializeField] private Sprite emptyHearth;
    [SerializeField] private Sprite fullHearth;

    public List<Image> hearths;

    private void Start()
    {
        mainCharacter = GameObject.FindWithTag("Player").gameObject.GetComponent<PlayerCombat>();
    }

    void Update()
    {
        switch (mainCharacter.CurrentHealth)
        {
            case 3:
                foreach (Image hearth in hearths)
                {
                    hearth.sprite = fullHearth;
                }
                break;
            case 2:
                hearths[2].sprite = emptyHearth;
                break;
            case 1:
                hearths[2].sprite = emptyHearth;
                hearths[1].sprite = emptyHearth;
                break;
            default:
                foreach (Image hearth in hearths)
                {
                    hearth.sprite = emptyHearth;
                }
                break;

        }
    }
}
