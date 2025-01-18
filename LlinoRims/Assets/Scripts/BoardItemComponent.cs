using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardItemComponent : MonoBehaviour
{
    public CellType cellType;

    private List<Transform> _myDoors = new List<Transform>();

    [SerializeField] private Sprite _pressedSprite;
    [SerializeField] private Sprite _unpressedSprite;   
    
    private Image _myImage;
    private List<CellComponent> _cellComponent = new List<CellComponent>();
    
    private Sequence _sequence;

    public void AddDoor(Transform door,CellComponent cellComponent)
    {
        _myDoors.Add(door);
        _cellComponent.Add(cellComponent);
    }
    
    private void Start()
    {
        _sequence = DOTween.Sequence();
        switch (cellType)
        {
            case CellType.Saw:
                _sequence.Append(transform.DOScale(new Vector3(3f, 3f, 3f), 0.15f));
                _sequence.Append(transform.DOScale(new Vector3(1f, 1f, 1f), 3f));
                _sequence.Append(transform.DOScale(new Vector3(1f, 1f, 1f), 1f));
                Destroy(GetComponent<Outline>());
                break;
            case CellType.Finish:
                _sequence.Append(transform.DOScaleY(1.8f, 1));
                _sequence.Append(transform.DOScaleY(1f, 1));
                _sequence.Append(transform.DOScaleX(1.8f, 1));
                _sequence.Append(transform.DOScaleX(1f, 1));
                break;
            case CellType.Wall:
                transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
                _sequence.Append(transform.DOScaleY(1.2f, 1));
                _sequence.Append(transform.DOScaleY(1f, 1));
                break;
            case CellType.Button:
                _myImage = GetComponent<Image>();
                _myImage.sprite = _unpressedSprite;
                break;
        }
        _sequence.SetLoops(-1, LoopType.Restart);
    }

    public void OpenDoors()
    {  
        _myImage.sprite = _pressedSprite;
        foreach (var item in _myDoors)
        {
            item.transform.DOScaleY(0, 0.1f);
            item.transform.DOScale(Vector3.zero, 0.1f);
        }

        foreach (var item in _cellComponent)
        {
            item.CellType = CellType.Nothing;
        }
    }
    
    private void LateUpdate()
    {
        switch (cellType)
        {
            case CellType.Saw:
                transform.Rotate(new Vector3(0, 0, -1), 360 * Time.deltaTime);
                break;
        }
    }
}
