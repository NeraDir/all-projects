using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastManager : MonoBehaviour
{
    [SerializeField]
    private float _borderOfLeft;

    [SerializeField] 
    private float _borderOfRight;

    [SerializeField]
    private float _beastStepRange;

    [SerializeField]
    private float _beastZSpeed;

    private Rigidbody _beastBody;

    private bool _cantMove;

    private void Start()
    {
        _beastBody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        _beastZSpeed += 0.001f;
        transform.position += new Vector3(0, 0, _beastZSpeed) * Time.deltaTime;
    }

    public void MoveToRight() 
    {
        if (_cantMove)
            return;
        if (transform.position.x + _beastStepRange > _borderOfRight)
            return;
        _cantMove = true;
        transform.DOMoveX(transform.position.x + _beastStepRange, 0.15f).OnComplete(() => _cantMove = false);
    }

    public void MoveToLeft() 
    {
        if (_cantMove)
            return;
        if (transform.position.x - _beastStepRange < _borderOfLeft)
            return;
        _cantMove = true;
        transform.DOMoveX(transform.position.x - _beastStepRange, 0.15f).OnComplete(() => _cantMove = false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CoinManager coin))
        {
            GameManager.Coins++;
            coin.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(coin.gameObject));
        }
        if (other.TryGetComponent(out RoadManager road))
        {
            GameManager.RoadSpawn?.Invoke(road.gameObject);
        }
        if (other.TryGetComponent(out TreeManager tree))
        {
            GameManager.PantherIsLoose?.Invoke();
        }
    }
}
