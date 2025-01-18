using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Animations : MonoBehaviour
{
    public static Animations Instance;

    [SerializeField] private CurrentCellAnimation cellAnimatePrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        DOTween.Init();
    }

    public void TranslateObject(Cell from, Cell to, bool isMerging)
    {
        Instantiate(cellAnimatePrefab, transform, false).Move(from, to, isMerging);
    }

    public void CreateObjectAnim(Cell cell)
    {
        Instantiate(cellAnimatePrefab, transform, false).Appear(cell);
    }
}
