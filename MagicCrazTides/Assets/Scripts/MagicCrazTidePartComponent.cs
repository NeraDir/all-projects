using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MagicCrazTidePartComponent : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRendere;

    [SerializeField] private List<Transform> childTransforms;

    [SerializeField] private Transform _parentY;

    [SerializeField] private GameObject _destructionEffect;

    private float _yValue;

    private Vector3 _startPos;

    private AudioClip _clip;
    private AudioClip _mClip;

    private void Start()
    {
        _clip = Resources.Load("Audio/Destruction") as AudioClip;
        _mClip = Resources.Load("Audio/Move") as AudioClip;
      
        _startPos = childTransforms[0].position;
    }

    private void LateUpdate()
    {
        if (_parentY != null)
            _yValue = _parentY.position.y;
        _lineRendere.positionCount = childTransforms.Count;
        for (int i = 0; i < _lineRendere.positionCount; i++)
        {
            _lineRendere.SetPosition(i, new Vector3(childTransforms[i].position.x, _yValue, childTransforms[i].position.z));
        }
    }

    public void Destruction(FruitType type)
    {
        if (type != MagicCrazTideGameManager.order[0])
        {
            MagicCrazTideSettingsManager.playSound?.Invoke(_mClip);
            Vector3 targetPosition = childTransforms[0].position + (childTransforms[1].position - childTransforms[0].position) / 2;
            targetPosition.y = _yValue;
            childTransforms[0].DOMove(targetPosition, 0.5f).OnComplete(() =>
            {
                MagicCrazTideSettingsManager.playSound?.Invoke(_mClip);
                childTransforms[0].DOMove(_startPos, 0.5f).OnComplete(() => 
                {
                    Transform target = MagicCrazTideGameManager.TempPlaces.Find(x => x.childCount <= 0);
                    MagicCrazTideFruitBlockComponent fruitBlock = childTransforms[0].GetChild(0).GetComponent<MagicCrazTideFruitBlockComponent>();
                    fruitBlock.transform.parent = null;
                    MagicCrazTideSettingsManager.playSound?.Invoke(_mClip);
                    fruitBlock.transform.DOMove(target.position, 0.5f).OnComplete(() => { fruitBlock.isPressed = false;fruitBlock.transform.parent = target; });
                });
            });
        }
        else
        {
            

            MoveFirstChildThroughOthers();
        }
        
    }

    void MoveFirstChildThroughOthers()
    {
        MagicCrazTideSettingsManager.playSound?.Invoke(_mClip);
        if (childTransforms.Count < 2)
        {
            childTransforms[0].DOScale(Vector3.zero, 0.25f).OnComplete(() =>
            {
                Instantiate(_destructionEffect, childTransforms[0].position, Quaternion.identity);
                Destroy(childTransforms[0].gameObject);
                childTransforms.Clear();
                MagicCrazTideGameManager.DestructedCount += 1;
                MagicCrazTideGameManager.onShowEnd?.Invoke();
                MagicCrazTideSettingsManager.playSound?.Invoke(_clip);
                MagicCrazTideGameManager.order.RemoveAt(0);
                Destroy(gameObject);
            });
            return;
        }

        childTransforms[0].DOMove(childTransforms[1].position, 0.5f).OnComplete(() =>
        {
            Destroy(childTransforms[1].gameObject);
            childTransforms.RemoveAt(1);

            MoveFirstChildThroughOthers();
        });
    }
}
