using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaseSlotComponent : MonoBehaviour
{
    [SerializeField] private Transform _lockPanel;

    [SerializeField] private Sprite[] _itemSprites;

    private Image _image;
    private Sprite _sprite;


    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void UpdateVisual() 
    {
        _lockPanel.localScale = Vector3.one;
        _lockPanel.GetComponent<Image>().DOFade(1, 0.1f);
        _lockPanel.localPosition = Vector3.zero;
        _lockPanel.rotation = Quaternion.identity;
    }

    public void OpenCell()
    {
        _lockPanel.DOLocalMoveX(_lockPanel.transform.localPosition.x + Random.Range(-20f, 20f), 0.25f);
        _lockPanel.DOLocalMoveY(_lockPanel.transform.localPosition.y + Random.Range(10f, 20), 0.25f).OnComplete(() =>
        {
            _lockPanel.DOLocalMoveY(_lockPanel.transform.localPosition.y - Random.Range(100, 500), 0.25f);
            _lockPanel.GetComponent<Image>().DOFade(0, 0.25f);
            _lockPanel.DOScale(Vector3.zero, 0.25f);
        });
    }

    public Sprite GetSprite() => _sprite;

    public void SetData()
    {
        _sprite = _itemSprites[Random.Range(0, _itemSprites.Length)];
        _image.sprite = _sprite;
    }
}
