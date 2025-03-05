using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Result : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText; 

    // Start is called before the first frame update
    void Start()
    {
        finalScoreText.text = "Score: " + ScoreManager.Instance.GetScore().ToString(); 
    }
}
