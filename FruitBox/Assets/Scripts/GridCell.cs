using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] 
    TextMeshProUGUI numberText; 

    void Awake()
    {
        SetRandomNumber(); 
    }

    public void ClearCell()
    {
        numberText.text = 0.ToString();
        Color color = Color.white;
        color.a = 0f;
        numberText.color = color; 
    }

    public void SetRandomNumber()
    {
        int rand = Random.Range(1, 9);
        Color color = Color.black;
        color.a = 1f; 
        numberText.text = rand.ToString();
    }

    public int GetNumber()
    {
        return int.Parse(numberText.text); 
    }
}
