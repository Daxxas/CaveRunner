using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private int coinDropAmount = 0;
    [SerializeField] private GameObject coin;
    [SerializeField] private Vector2 coinForce;
    [SerializeField] private float offsetRange = 1f;
    
    private void OnDestroy()
    {
        for (int i = 0; i < coinDropAmount; i++)
        {
            float offset = Random.Range(-offsetRange, offsetRange);
            GameObject thisCoin = Instantiate(coin, new Vector3(transform.position.x + offset, transform.position.y + 0.5f, transform.position.z),
                transform.rotation);
            
            Vector2 externalForce = new Vector2(coinForce.x * Mathf.Sign(offset), coinForce.y);
            
            thisCoin.GetComponent<MiscController>().ExternalMove(externalForce);
        }
    }
}
