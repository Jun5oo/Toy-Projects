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
            // 기존에 선택된 셀들을 릴리스 
            BoardManager.Instance.ClearSelected();

            // 드래그 박스의 스크린 좌표 
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

        // Mathf.Abs, 절대값을 씌워준 이유는 height, width 값이 음수 값이 되는 것을 방지하기 위함 
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2); 
    }


}
