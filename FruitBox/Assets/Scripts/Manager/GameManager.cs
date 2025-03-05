using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private bool isGameStarted;
    private bool isGameFinished; 

    public static GameManager Instance { get; private set; }
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this); 
        }
        else
        {
            Destroy(this);
            return; 
        }
    }

    void Start()
    {
        Init(); 
    }

    public bool IsGameStarted() => isGameStarted;

    public bool IsGameFinished() => isGameFinished;
    public void StartGame()
    {
        isGameStarted = true;

        UIManager.Instance.UnActiveTitleUI(); 
        BoardManager.Instance.CreateBoard();
        UIManager.Instance.ActiveGameUI(); 
    }

    public void EndGame()
    {
        isGameFinished = true;
        UIManager.Instance.ActiveGameEndUI(); 
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Init(); 
    }

    public void Init()
    {
        isGameStarted = false;
        isGameFinished = false; 
    }
}
