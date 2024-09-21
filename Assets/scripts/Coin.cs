using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : MonoBehaviour
{
    public CoinsCount CoinsCountScript;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider that entered the trigger is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Destroy the coin GameObject to remove it from the scene
            Destroy(gameObject);
            CoinsCountScript.Addcount();
            Debug.Log("coin collected");
        }
    }
}
