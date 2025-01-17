using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderPeoplesComponents : MonoBehaviour,ICollisionableComponent
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private WonderObjectsToMove _objectToMoveComponent;

    private bool _isMoving = false;

    public void Use(Transform target)
    {
        if (_isMoving)
            return;
        _isMoving = true;
        transform.parent = target;
        transform.DOLocalRotateQuaternion(Quaternion.Euler(transform.rotation.x, transform.rotation.y + 90, transform.rotation.z), 0.25f);
        Destroy(_objectToMoveComponent);
        _animator.SetBool("WonderPeopleAnimationKey", true);
        transform.DOMoveY(target.transform.position.y - 0.5f, 12).
            OnComplete(() => transform.DOScale(Vector3.zero, 0.25f).
                OnComplete(() => {Destroy(gameObject); GameManager.wonderHelpedPeoplesCount++; }));
    }


}
