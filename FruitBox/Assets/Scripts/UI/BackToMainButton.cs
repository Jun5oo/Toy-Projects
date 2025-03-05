using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainButton : MonoBehaviour
{
    public void BackToMain()
    {
        GameManager.Instance.ReturnToTitle(); 
    }
}
