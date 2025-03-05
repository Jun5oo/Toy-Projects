using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectableUI : MonoBehaviour
{
    private bool selectEnable; 

    void Awake()
    {
        selectEnable = true; 
    }

    public void OnSelected() => Debug.Log("Selected"); 

    public void DeSelected() => Debug.Log("DeSelected");

    public bool IsSelectionEnable() => selectEnable; 

    public void EnableSelection()
    {
        selectEnable = true; 
    }

    public void DisableSelection()
    {
        selectEnable = false;
        this.gameObject.GetComponent<GridCell>().ClearCell(); 
    }
}
