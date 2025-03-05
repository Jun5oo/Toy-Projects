using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] TextMeshProUGUI titleUI; 
    [SerializeField] TextMeshProUGUI scoreUI;
    [SerializeField] TextMeshProUGUI timerUI;
    
    [SerializeField] GameObject finalScoreUI;

    [SerializeField] Slider sliderUI;
    [SerializeField] Timer timer;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this); 
    }

    void Start()
    {
        sliderUI.maxValue = timer.GetDuration(); 
    }

    public Rect GetScreenRect(RectTransform rectTransform)
    {
        Vector3[] worldCorner = new Vector3[4];
        // 로컬 좌표게 rectTransform의 네 개의 모서리 좌표를 월드 좌표로 변환 
        rectTransform.GetWorldCorners(worldCorner);

        Vector2 worldMin = worldCorner[0];
        Vector2 worldMax = worldCorner[2];

        // 좌하단, 우상단 좌표를 스크린 좌표로 변환 (RectTransform 역시 마찬가지로 좌하단을 기준으로 0, 0) 
        Vector2 screenMin = RectTransformUtility.WorldToScreenPoint(null, worldMin);
        Vector2 screenMax = RectTransformUtility.WorldToScreenPoint(null, worldMax);

        return new Rect(screenMin, screenMax - screenMin);
    }

    public void FindOverlapped(RectTransform selectionBox)
    {
        Rect selectionRect = GetScreenRect(selectionBox);

        foreach (SelectableUI available in BoardManager.Instance.GetAvailableCells())
        {
            // 각 셀의 스크린 좌표
            RectTransform cell = available.gameObject.GetComponent<RectTransform>();
            Rect cellRect = GetScreenRect(cell);

            // 겹치는 경우 해당 셀은 selectedUI에 추가 
            if (selectionRect.Overlaps(cellRect) && available.IsSelectionEnable())
                BoardManager.Instance.AddToSelected(available);
        }
    }

    public void UpdateTimerUI(float elapsedTime)
    {
        elapsedTime = Mathf.Max(0, elapsedTime); 

        float minutes = Mathf.FloorToInt(elapsedTime / 60);
        float seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        sliderUI.value = elapsedTime; 
    }

    public void UpdateScoreUI(int score) => scoreUI.text = score.ToString();
    public void ActiveTitleUI() => titleUI.gameObject.SetActive(false);
    public void UnActiveTitleUI() => titleUI.gameObject.SetActive(false); 
    public void ActiveGameUI()
    {
        scoreUI.gameObject.SetActive(true);
        timerUI.gameObject.SetActive(true);
        sliderUI.gameObject.SetActive(true);
    }

    public void UnActiveGameUI()
    {
        scoreUI.gameObject.SetActive(false);
        timerUI.gameObject.SetActive(false);
        sliderUI.gameObject.SetActive(false);
    }

    public void ActiveGameEndUI()
    {
        finalScoreUI.gameObject.SetActive(true); 
    }
}
