using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    GameManager gameManager;

    void OnEnable()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("No GameManager was Found");
            return;
        }
    }
    public void OnClick()
    {
        gameManager.isGameStarted = true;
    }

    public void Update()
    {
        if (gameManager.isGameStarted)
        {
            gameObject.SetActive(false);
            return;
        }
    }
}
