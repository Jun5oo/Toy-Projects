using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private RectTransform selectionBox;

    private Vector2 startPos;
 
    void Update()
    {
        if (GameManager.Instance.IsGameFinished())
            return; 

        if (!GameManager.Instance.IsGameStarted())
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                GameManager.Instance.StartGame();

            return; 
        }

        HandleSelection(); 
    }

    void HandleSelection()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startPos = Input.mousePosition;
            selectionBox.sizeDelta = Vector2.zero;
            selectionBox.gameObject.SetActive(true);
        }

        else if (Input.GetKey(KeyCode.Mouse0))
        {
            ResizeSelectionBox();
        }

        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            // ������ ���õ� ������ ������ 
            BoardManager.Instance.ClearSelected();

            // �巡�� �ڽ��� ��ũ�� ��ǥ 
            UIManager.Instance.FindOverlapped(selectionBox);

            if (GameRule.IsSumCorrect(BoardManager.Instance.GetSelectedCells()))
            {
                ScoreManager.Instance.AddScore(BoardManager.Instance.GetSelectedCells().Count);
                BoardManager.Instance.DisableAllSelectedCells(); 
            }

            selectionBox.gameObject.SetActive(false); 
        }
    }

    void ResizeSelectionBox()
    {
        float width = Input.mousePosition.x - startPos.x;
        float height = Input.mousePosition.y - startPos.y;

        // Mathf.Abs, ���밪�� ������ ������ height, width ���� ���� ���� �Ǵ� ���� �����ϱ� ���� 
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2); 
    }


}
