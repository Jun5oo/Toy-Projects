using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    GameManager gameManager;

    Image img; 
    Fade fade;

    bool isFading; 

    void OnEnable()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        img = GetComponent<Image>();
        Color color = img.color;
        color.a = 1;
        img.color = color; 

        fade = GetComponent<Fade>();
        isFading = false; 

        if(gameManager == null)
        {
            Debug.LogError("No GameManager was Found");
            return; 
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameStarted)
        {
            if (!isFading)
            {
                isFading = true;
                fade.FadeOut(0.1f); 
            }

            if(img.color.a <= 0)
                gameObject.SetActive(false);

            return; 
        }

    }

}
