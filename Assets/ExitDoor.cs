using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    [SerializeField] private LayerMask whatIsPlayer;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D AtDoor = Physics2D.OverlapCircle(transform.position, 0.5f, whatIsPlayer);


        if (AtDoor != null)
        {
            if (AtDoor.GetComponent<PlayerInputEvents>().Action)
            {
                levelManager.DungeonFinished();
            }
        }
    }
}
