using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WondoerLootItemComponent : MonoBehaviour, ICollisionableComponent
{
    [SerializeField]
    private WonderObjectsToMove _objectToMoveComponent;

    private bool _isMoving = false;

    private Vector3 _beginScale;

    private void Awake()
    {
        _beginScale = transform.localScale;
    }

    public void Use(Transform target)
    {
        if (_isMoving)
            return;
        _isMoving = true;
        Destroy(_objectToMoveComponent);
        transform.parent = target;
        transform.DOMoveY(target.position.y, 12f).
                    OnComplete(() => transform.DOScale(Vector3.zero, 0.25f).
                        OnComplete(() => { Destroy(gameObject); GameManager.wonderPlaneHealth += 10f; })); 

        
    }
}
