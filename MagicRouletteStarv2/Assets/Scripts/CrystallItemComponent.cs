using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CrystallType
{
    Red = 0, 
    Blue = 1,
    Green = 2, 
    Pink = 3, 
    Purple = 4, 
    Yellow = 5
}

public class CrystallItemComponent : MonoBehaviour, IPointerClickHandler
{
    public CrystallType type;

    private bool _isClicked;

    [SerializeField] private Sprite[] _gemSprites;

    private Image _image;

    private Vector3 _direction;

    private void Start()
    {
        _image = GetComponent<Image>();
        type = (CrystallType)Random.Range(0, 6);
        _image.sprite = _gemSprites[(int)type];
        if (transform.localPosition.x > 0)
        {
            _direction = new Vector3(-1, 0, 0);
        }
        else if (transform.localPosition.x < 0)
        {
            _direction = new Vector3(1, 0, 0);
        }
    }

    private void LateUpdate()
    {
        transform.position += _direction * GameComponent.speed * Time.deltaTime;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isClicked)
            return;
        _isClicked = true;
        transform.DOScale(Vector3.one / 1.3f, 0.1f).OnComplete(() => transform.DOScale(Vector3.one * 1.2f, 0.1f).OnComplete(() => transform.DOScale(Vector3.one, 0.1f).OnComplete(() => OnClicked())));
    }

    private void OnClicked()
    {
        GameComponent.onCheckCrystall?.Invoke(type);
        Destroy(gameObject);
    }
}
