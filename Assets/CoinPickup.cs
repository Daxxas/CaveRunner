using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsPlayer;
    private Animator animator;
    
    private bool pickedup = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D onCoin = Physics2D.OverlapCircle(transform.position, 0.5f, whatIsPlayer);

        CoinHUD scoreHUD = FindObjectOfType<CoinHUD>();
        
        if (onCoin != null && !pickedup) 
        {
            scoreHUD.AddScore(1);
            pickedup = true;
            FindObjectOfType<SoundManager>().Play("Collect");
            animator.SetTrigger("pickedup");
            //Destroy dans l'animator
        }
    }
}
