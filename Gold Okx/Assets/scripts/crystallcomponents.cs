using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class crystallcomponents : MonoBehaviour
{
    public int index;
    private Sprite _mineSprite;
    private float _mineScale;
    private int _mineScore;

    public bool isTriggered = false;

    private Image _mineImage;

    public static UnityEvent<int> starGetted = new UnityEvent<int>();


    public void SetData(int indexe, Sprite sprite,float scale,int score)
    {
        _mineImage = GetComponent<Image>();
        index = indexe;
        _mineSprite = sprite;
        _mineScale = scale;
        _mineScore = score;
        _mineImage.sprite = _mineSprite;
        transform.localScale = new Vector3(_mineScale, _mineScale, _mineScale);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out crystallcomponents crystall))
        {
            if (crystall.index == index)
            {
                if (isTriggered)
                    return;
                isTriggered = true;
                crystall.isTriggered = true;
                crystall.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { Destroy(gameObject); });
                transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { starGetted?.Invoke(_mineScore); Destroy(gameObject); });
            }
        }
    }
}
