using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public static Field Instance;

    public float Spacing;
    public float CellSizePrefab;
    public int FieldSize;

    [SerializeField] private Cell CellPrefab;
    [SerializeField] private RectTransform rect;

    private Cell[,] fieldDesk;
    private bool isAnyCellMoved;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if (SwipeDetectOnMobile.instance.SwipeLeft)
            OnInput(Vector2.left);
        if (SwipeDetectOnMobile.instance.SwipeRight)
            OnInput(Vector2.right);
        if (SwipeDetectOnMobile.instance.SwipeUp)
            OnInput(Vector2.up);
        if (SwipeDetectOnMobile.instance.SwipeDown)
            OnInput(Vector2.down);
    }

    private void OnInput(Vector2 direction)
    {
        if (!GameManager.GameStarted) return;

        isAnyCellMoved = true;
        RemoveCellsFlags();

        Move(direction);

        if (isAnyCellMoved)
        {
            CreateRandomCell();
            CheckGameResults();
        }
    }

    private Cell FindCellWhatToMerge(Cell cell, Vector2 direction)
    {
        int startX = cell.X + (int)direction.x;
        int startY = cell.Y - (int)direction.y;

        for (int x = startX, y = startY; x >= 0 && x < FieldSize && y >= 0 && y < FieldSize; x += (int)direction.x, y -= (int)direction.y)
        {
            if (fieldDesk[x, y].IsEmpty) continue;

            if (fieldDesk[x, y].Value == cell.Value && !fieldDesk[x, y].HasMerged)
                return fieldDesk[x, y];

            break;
        }

        return null;
    }

    private Cell FindEmptyCEll(Cell cell, Vector2 direction)
    {
        int startX = cell.X + (int)direction.x;
        int startY = cell.Y - (int)direction.y;
        Cell emptyCell = null;

        for (int x = startX, y = startY; x >= 0 && x < FieldSize && y >= 0 && y < FieldSize; x += (int)direction.x, y -= (int)direction.y)
        {
            if (fieldDesk[x, y].IsEmpty)
                emptyCell = fieldDesk[x, y];
            else
                break;
        }

        return emptyCell;
    }

    private void CheckGameResults()
    {
        bool lose = true;

        for (int x = 0; x < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                if (fieldDesk[x, y].Value == 10)
                {
                    GameManager.Instance.Win();

                    return;
                }

                if (lose &&
                    fieldDesk[x, y].IsEmpty ||
                    FindCellWhatToMerge(fieldDesk[x, y], Vector2.left) ||
                    FindCellWhatToMerge(fieldDesk[x, y], Vector2.right) ||
                    FindCellWhatToMerge(fieldDesk[x, y], Vector2.up) ||
                    FindCellWhatToMerge(fieldDesk[x, y], Vector2.down))
                {
                    lose = false;
                }
            }
        }

        if (lose)
            GameManager.Instance.Lose();
    }

    public void CreateFieldDesk()
    {
        fieldDesk = new Cell[FieldSize, FieldSize];

        float fieldSizeBuff = FieldSize * (CellSizePrefab + Spacing) + Spacing;
        rect.sizeDelta = new Vector2(fieldSizeBuff, fieldSizeBuff);

        float startX = -(fieldSizeBuff / 2) + (CellSizePrefab / 2) + Spacing;
        float startY = (fieldSizeBuff / 2) - (CellSizePrefab / 2) - Spacing;

        for (int x = 0; x < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                var cell = Instantiate(CellPrefab, transform, false);
                var position = new Vector2(startX + (x * (CellSizePrefab + Spacing)), startY - (y * (CellSizePrefab + Spacing)));

                cell.transform.localPosition = position;
                fieldDesk[x, y] = cell;
                cell.SetCellValue(x, y, 0);
            }
        }
    }

    private void Move(Vector2 direction)
    {
        int startXY = direction.x > 0 || direction.y < 0 ? FieldSize - 1 : 0;
        int dir = direction.x != 0 ? (int)direction.x : -(int)direction.y;

        for (int i = 0; i < FieldSize; i++)
        {
            for (int k = startXY; k >= 0 && k < FieldSize; k -= dir)
            {
                var cell = direction.x != 0 ? fieldDesk[k, i] : fieldDesk[i, k];

                if (cell.IsEmpty) continue;

                var cellToMerge = FindCellWhatToMerge(cell, direction);
                if (cellToMerge != null)
                {
                    cell.MergeWithCell(cellToMerge);
                    isAnyCellMoved = true;

                    continue;
                }

                var emptyCell = FindEmptyCEll(cell, direction);
                if (emptyCell != null)
                {
                    cell.MoveToCell(emptyCell);
                    isAnyCellMoved = true;
                }
            }
        }
    }

    public void CreateRandomCell()
    {
        var emptyCelles = new List<Cell>();

        for (int x = 0; x < FieldSize; x++)
            for (int y = 0; y < FieldSize; y++)
                if (fieldDesk[x, y].IsEmpty)
                    emptyCelles.Add(fieldDesk[x, y]);

        if (emptyCelles.Count == 0)
            return;

        int value = Random.Range(0, 10) == 0 ? 2 : 1;
        var cellBuff = emptyCelles[Random.Range(0, emptyCelles.Count)];
        cellBuff.SetCellValue(cellBuff.X, cellBuff.Y, value, false);

        Animations.Instance.CreateObjectAnim(cellBuff);
    }

    public void CreateField()
    {
        if (fieldDesk == null)
            CreateFieldDesk();

        for (int x = 0; x < FieldSize; x++)
            for (int y = 0; y < FieldSize; y++)
                fieldDesk[x, y].SetCellValue(x, y, 0);

        CreateRandomCell();
        CreateRandomCell();
    }

    private void RemoveCellsFlags()
    {
        for (int x = 0; x < FieldSize; x++)
            for (int y = 0; y < FieldSize; y++)
                fieldDesk[x, y].ResetFlgas();
    }
}
