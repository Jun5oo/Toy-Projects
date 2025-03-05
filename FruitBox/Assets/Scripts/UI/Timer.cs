using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float elapsedTime;

    // public event Action<float> OnTimeChanged;
    // public event Action OnTimeUp; 

    void Start()
    {
        elapsedTime = duration; 
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGameStarted())
            return;

        if (GameManager.Instance.IsGameFinished())
            return; 

        elapsedTime -= Time.deltaTime;
        // OnTimeChanged.Invoke(elapsedTime);
        UIManager.Instance.UpdateTimerUI(elapsedTime);

        if (elapsedTime <= 0)
            GameManager.Instance.EndGame(); 
    }

    public float GetDuration() => duration; 
}
