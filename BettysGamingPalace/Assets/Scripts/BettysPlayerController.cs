using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettysPlayerController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private GameObject _blockPrefab;

    [SerializeField] private Transform _spawnPosition;

    [SerializeField] private Transform[] MinMax;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        
    }

    public void OnClickDoMove(int value)
    {
        float pos = transform.position.x + value * 2;
        pos = Mathf.Clamp(pos, MinMax[0].position.x, MinMax[1].position.x);
        transform.DOMoveX(pos, 0.25f).OnComplete(() => _animator.SetInteger("PlayerIndex", 0));
        _animator.SetInteger("PlayerIndex", value > 0 ? 2 : 1);
        Instantiate(_blockPrefab, _spawnPosition.position, Quaternion.identity);
    }
}
