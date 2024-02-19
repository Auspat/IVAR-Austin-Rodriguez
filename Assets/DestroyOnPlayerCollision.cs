using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPlayerCollision : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("coin1"))
        {
            gameManager.coin1++;
        }
        if(other.gameObject.CompareTag("coin2"))
        {
            gameManager.coin2++;
        }
        if(other.gameObject.CompareTag("coin3"))
        {
            gameManager.coin3++;
        }
        //Destroy(other.gameObject);
    }
}
