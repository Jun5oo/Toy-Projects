using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlappyBird : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject fadeImage; 

    Rigidbody2D rb;
    BoxCollider2D bc; 
    Animator anim; 

    Vector2 maxVelocity = new Vector2(0, 5);

    float maxAngle = 30;
    float minAngle = -90;

    bool inputEnable; 

    bool startFlag;
    bool endFlag; 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        inputEnable = true; 

        startFlag = false;
        endFlag = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameStarted)
            return;

        if (!startFlag)
        {
            startFlag = true;
            StartCoroutine(waitCoroutine(0.5f));
        }

        if (gameManager.isGameOver)
        {
            if (this.transform.position.y < -20)
                this.gameObject.SetActive(false); 

            anim.enabled = false;
            bc.isTrigger = true;

            if (!endFlag)
            {
                endFlag = true;
                StartCoroutine(waitCoroutine(1.5f));
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && inputEnable) 
            gameManager.AccelerateSpeed(); 

        if(Input.GetKeyUp(KeyCode.LeftShift) && inputEnable)
            gameManager.DecelerateSpeed(); 

        if (Input.GetKeyDown(KeyCode.Space) && inputEnable)
        {
            rb.velocity = Vector2.zero; 
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

            if (rb.velocity.y > 5)
                rb.velocity = maxVelocity;
        }

        if (rb.velocity.y > 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, maxAngle), 0.1f);

        if (rb.velocity.y < 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, minAngle), 0.015f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Ground")) && !gameManager.isGameOver)
        {
            gameManager.isGameOver = true;
            inputEnable = false;
            fadeImage.GetComponent<Fade>().FadeInOut(0.05f); 
        }

        if (collision.gameObject.CompareTag("Bound"))
        {
            rb.velocity = Vector2.zero; 
        }
    }

    IEnumerator waitCoroutine(float seconds)
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(seconds);

        rb.gravityScale = 1f; 
    }
}
