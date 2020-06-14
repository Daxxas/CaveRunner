using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound List", menuName = "Sound List")]
public class Sound : ScriptableObject
{
    [HideInInspector] public AudioSource source;
    
    public AudioClip sound;
    public string name;
    public bool loop = false;

    [Range(0f, 1f)]
    public float volume = 1f;
}
