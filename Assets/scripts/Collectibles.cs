using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public CoinsCount CoinsCountScript;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Destroy(gameObject);
            CoinsCountScript.Addcount();
            Debug.Log("coin collected");
        }
    }
}
