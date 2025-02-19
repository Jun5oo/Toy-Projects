using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    Image img;
    bool isFadeOut;
    bool isFadeIn; 

    void Awake()
    {
        img = GetComponent<Image>();
        isFadeOut = true;
        isFadeIn = false; 
    }

    public void FadeIn(float seconds)
    {
        StartCoroutine(CoFadeIn(seconds)); 
    }
    public void FadeOut(float seconds)
    {
        StartCoroutine(CoFadeOut(seconds));
    }

    public void FadeInOut(float seconds)
    {
        StartCoroutine(CoFadeInOut(seconds)); 
    }

    IEnumerator CoFadeIn(float seconds)
    {
        Color color = img.color; 

        while(img.color.a <= 1)
        {
            color.a += seconds;
            yield return new WaitForSeconds(0.001f); 
            img.color = color;
        }

        isFadeOut = false;
        isFadeIn = true; 
    }

    IEnumerator CoFadeOut(float seconds)
    {
        Color color = img.color;

        while (img.color.a >= 0)
        {
            color.a -= seconds;
            yield return new WaitForSeconds(0.001f);
            img.color = color;
        }

        isFadeOut = true;
        isFadeIn = false; 
    }

    IEnumerator CoFadeInOut(float seconds)
    {
        StartCoroutine(CoFadeIn(seconds));
        
        while (!isFadeIn)
        {
            yield return null; 
        }

        StartCoroutine(CoFadeOut(seconds)); 
    }
}
