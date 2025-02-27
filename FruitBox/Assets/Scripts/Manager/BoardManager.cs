using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    [SerializeField] GridLayoutGroup grid;
    [SerializeField] GameObject gridElement;
    [SerializeField] int cellSizeX;
    [SerializeField] int cellSizeY;
    [SerializeField] int cellNum;
    [SerializeField] int boardWidth;

    public static BoardManager Instance
    {
        get; private set;
    }

    private List<SelectableUI> availableCells = new List<SelectableUI>();
    private HashSet<SelectableUI> selectedCells = new HashSet<SelectableUI>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        CreateBoard();
    }

    public void CreateBoard()
    {
        grid.cellSize = new Vector2(cellSizeX, cellSizeY);
        grid.childAlignment = TextAnchor.MiddleCenter;
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = boardWidth;

        for (int i = 0; i < cellNum; i++)
        {
            GameObject cell = GameObject.Instantiate(gridElement);
            cell.transform.SetParent(this.transform);
            AddToAvailable(cell.GetComponent<SelectableUI>());
        }
    }

    public void AddToAvailable(SelectableUI cell) => availableCells.Add(cell);

    public void AddToSelected(SelectableUI cell) => selectedCells.Add(cell); 

    public void RemoveFromSelected(SelectableUI cell) => selectedCells.Remove(cell);

    public void ClearSelected() => selectedCells.Clear();

    public List<SelectableUI> GetAvailableCells() => availableCells;

    public HashSet<SelectableUI> GetSelectedCells() => selectedCells; 

    public void DisableAllSelectedCells()
    {
        foreach(SelectableUI cell in selectedCells)
        {
            cell.DisableSelection(); 
        }
    }

}
