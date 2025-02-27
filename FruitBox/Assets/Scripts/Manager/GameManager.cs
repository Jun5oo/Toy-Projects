using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get;
        private set; 
    }

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

    void StartGame()
    {

    }

    void EndGame()
    {

    }

    void RestartGame()
    {

    }
}
