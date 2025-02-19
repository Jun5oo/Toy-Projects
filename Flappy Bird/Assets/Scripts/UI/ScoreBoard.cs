using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    GameManager gameManager;

    bool flag;
    bool upEnable;

    RectTransform rect; 

    public Image medalUI;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI bestScoreUI;

    public Sprite bronzeMedal;
    public Sprite silverMedal;
    public Sprite goldMedal; 


    void OnEnable()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        if (gameManager == null)
        {
            Debug.LogError("No GameManager was Found");
            return;
        }
        
        flag = false;
        upEnable = false; 

        rect = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.isGameOver)
        {
            if(!flag)
                StartCoroutine(coWait()); 

            if(rect.anchoredPosition.y < 0 && upEnable)
            {
                rect.position += Vector3.up * 10f;  
            }
        }
    }

    public void UpdateScoreBoard()
    {
        int result = gameManager.GetResultScore();
        int best = gameManager.GetBestScore(); 

        scoreUI.text = result.ToString();
        bestScoreUI.text = best.ToString();

        UpdateMedal(result); 
    }

    public void UpdateMedal(int result)
    {
        if (result < 10)
            medalUI.sprite = bronzeMedal;

        else if (result < 20)
            medalUI.sprite = silverMedal;

        else
            medalUI.sprite = goldMedal; 
    }   

    IEnumerator coWait()
    {
        flag = true; 
        yield return new WaitForSeconds(2.5f);
        upEnable = true; 
    }
}
