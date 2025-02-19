using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdDetector : MonoBehaviour
{
    GameManager gameManager;

    void OnEnable()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        if (gameManager == null)
        {
            Debug.LogError("No gameManager was Found");
            return; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameManager.isAccelerated)
                gameManager.UpdateScore(2);
            else
                gameManager.UpdateScore(1); 
        }
    }
}
