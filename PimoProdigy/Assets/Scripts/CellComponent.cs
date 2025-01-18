using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellComponent : MonoBehaviour, IPointerClickHandler
{
    private bool _isClicked;
    public bool isRock;
    public bool isFinish;
    public bool isFirst;

    [SerializeField]
    private Image _cellImage;

    [SerializeField]
    private Sprite _rock;

    [SerializeField]
    private Sprite _finish;

    private void Start()
    {
        if(isFinish)
            _cellImage.sprite = _finish;
        if(isRock)
            _cellImage.sprite = _rock;
    }

    public void Init(Sprite sprite)
    {
        _cellImage.sprite = sprite;
    }

    public void OnSelectChange(bool value)
    {
        _isClicked = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameController.selectedCells.Count > 0 && Vector3.Distance(GameController.selectedCells[0].transform.position, transform.position) > 0.8f)
            return;
        if (GameController.selectedCells.Count == 0 && isFinish)
            return;
        if (isRock)
            return;
        if (_isClicked)
            return;
        if (GameController.canMove)
            return;
        _isClicked = true;
        transform.DOScale(1.3f, 0.1f);
        GameController.selectedCells.Add(this);
        if (GameController.selectedCells.Count >= 2)
        {
            GameController.canMove = true;
            GameController.moveCells?.Invoke();
        }
    }
}
