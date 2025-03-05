using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;

    public static ScoreManager Instance { get; private set;  }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        UpdateScore(); 
    }

    public void ResetScore() => score = 0; 
    public void AddScore(int cellNum)
    {
        score += cellNum;
        UpdateScore(); 
    }

    public int GetScore() => score;

    public void UpdateScore() => UIManager.Instance.UpdateScoreUI(GetScore()); 

}
