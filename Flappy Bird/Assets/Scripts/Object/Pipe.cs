using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameManager gameManager;

    Rigidbody2D rb; 

    public float startX = 10;
    public float startY; 

    float maxY = -0.5f;
    float minY = -4; 

    void OnEnable()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gameManager == null)
            return; 

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * gameManager.pipeSpeed;  

        startY = Random.Range(minY, maxY);
        transform.position = new Vector2(startX, startY); 
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameOver)
        {
            rb.velocity = Vector2.zero;
            return; 
        }

        rb.velocity = Vector2.left * gameManager.pipeSpeed; 

        if (transform.position.x < -10)
            this.gameObject.SetActive(false);
    }
}
