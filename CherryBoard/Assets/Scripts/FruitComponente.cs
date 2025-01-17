using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FruitComponente : MonoBehaviour, IPointerClickHandler
{
    private FruitsMoveDirections _direction;

    private float moveSpeed;
    private bool isdead;

    [SerializeField]
    private Image _fruitDisplay;

    [SerializeField]
    private Sprite[] _fruitSprites;

    public void Init(FruitsMoveDirections direction, float speed)
    {
        this._direction = direction;
        this.moveSpeed = speed;
        _fruitDisplay.sprite = _fruitSprites[Random.Range(0, _fruitSprites.Length)];
        Destroy(gameObject,20/((float)FruitGameManager.CurrentLevelValue/2));
    }

    private void FixedUpdate()
    {
        if (isdead)
            return;
        transform.position += Vector3.right * ((_direction == FruitsMoveDirections.Right ? 1 : -1) * moveSpeed);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isdead)
            return;
        isdead = true;
       
        if (_fruitDisplay.sprite != FruitGameManager.TargetFruitSprite)
        {
            FruitGameManager.CurrentScoreCount -= Random.Range(25, 100) * FruitGameManager.CurrentLevelValue;
        }
        else
        {
            FruitGameManager.CurrentScoreCount += Random.Range(25, 100) * FruitGameManager.CurrentLevelValue;
        }
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
        
    }
}
