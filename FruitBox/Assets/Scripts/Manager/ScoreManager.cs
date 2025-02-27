using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] TextMeshProUGUI scoreUI; 

    public static ScoreManager Instance { get; private set;  }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        UpdateScoreUI(); 
    }

    public void ResetScore() => score = 0; 
    public void AddScore(int cellNum)
    {
        score += cellNum;
        UpdateScoreUI(); 
    }

    public int GetScore() => score;

    public void UpdateScoreUI() => scoreUI.text = GetScore().ToString(); 

}
