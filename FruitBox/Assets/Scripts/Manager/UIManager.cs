using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this); 
    }

    public Rect GetScreenRect(RectTransform rectTransform)
    {
        Vector3[] worldCorner = new Vector3[4];
        // ���� ��ǥ�� rectTransform�� �� ���� �𼭸� ��ǥ�� ���� ��ǥ�� ��ȯ 
        rectTransform.GetWorldCorners(worldCorner);

        Vector2 worldMin = worldCorner[0];
        Vector2 worldMax = worldCorner[2];

        // ���ϴ�, ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ (RectTransform ���� ���������� ���ϴ��� �������� 0, 0) 
        Vector2 screenMin = RectTransformUtility.WorldToScreenPoint(null, worldMin);
        Vector2 screenMax = RectTransformUtility.WorldToScreenPoint(null, worldMax);

        return new Rect(screenMin, screenMax - screenMin);
    }

    public void FindOverlapped(RectTransform selectionBox)
    {
        Rect selectionRect = GetScreenRect(selectionBox);

        foreach (SelectableUI available in BoardManager.Instance.GetAvailableCells())
        {
            // �� ���� ��ũ�� ��ǥ
            RectTransform cell = available.gameObject.GetComponent<RectTransform>();
            Rect cellRect = GetScreenRect(cell);

            // ��ġ�� ��� �ش� ���� selectedUI�� �߰� 
            if (selectionRect.Overlaps(cellRect) && available.IsSelectionEnable())
                BoardManager.Instance.AddToSelected(available);
        }
    }
}
