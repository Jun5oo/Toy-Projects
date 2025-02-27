using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRule
{
    public static bool IsSumCorrect(HashSet<SelectableUI> selectedUI)
    {
        int sum = 0; 

        foreach(SelectableUI selected in selectedUI)
            sum += selected.gameObject.GetComponent<GridCell>().GetNumber();

        return sum == 10 ? true : false; 
    }
}
