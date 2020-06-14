using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffect : MonoBehaviour
{
    public void PlayStep1()
    {
        FindObjectOfType<SoundManager>().Play("Step1");
    }
    
    public void PlayStep2()
    {
        FindObjectOfType<SoundManager>().Play("Step2");
    }
}
