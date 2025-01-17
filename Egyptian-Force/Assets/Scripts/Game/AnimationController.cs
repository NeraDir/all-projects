using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance;

    public LevelItemSO CurrentLevel;
    [SerializeField] private CellAnimate cellAnimatePrefab;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        DOTween.Init();
    }

    private void Start()
    {
        cellAnimatePrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(CurrentLevel.Levels[GlobalSave.Level].CellSize, CurrentLevel.Levels[GlobalSave.Level].CellSize);
    }

    public void SmoothTransition(CellScript from, CellScript to, bool isMerging)
    {
        Instantiate(cellAnimatePrefab, transform, false).Move(from, to, isMerging);
    }

    public void SmoothAppear(CellScript cell)
    {
        Instantiate(cellAnimatePrefab, transform, false).Appear(cell);
    }
}
