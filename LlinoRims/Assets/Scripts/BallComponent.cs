using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    public static Action<Transform> moveTo;
    public static Action<bool> endBallLive;
    
    private bool _cantMove;
    private AudioClip _moveSound;
    
    private void Start()
    {
        moveTo += OnMove;
        _moveSound = Resources.Load<AudioClip>("Sounds/Move");
    }

    private void OnDestroy()
    {
        moveTo -= OnMove;
    }

    public bool GetBallState()
    {
        return _cantMove;
    }

    private void OnMove(Transform target)
    {
        if(_cantMove)
            return;
        _cantMove = true;
        BgSetter.playSound?.Invoke(_moveSound);
        transform.DOMove(target.position, 0.25f).OnComplete(() => _cantMove = false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BoardItemComponent boardItem))
        {
            if(GameController.isEnd)
                return;
            switch (boardItem.cellType)
            {
                case CellType.Saw:
                    endBallLive?.Invoke(false);
                    break;
                case CellType.Finish:
                    endBallLive?.Invoke(true);
                    break;
                case CellType.Button:
                    boardItem.OpenDoors();
                    break;  
            }
        }
    }
}
