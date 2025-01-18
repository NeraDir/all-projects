using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PimoTargetMove : MonoBehaviour
{
    public static float moveSpeed;
    private Transform _cup;
    private Transform _destroyLine;
    [SerializeField]
    private GameObject[] _enables;
    [SerializeField]
    private Transform _ball;
    private int _enablesCheck;
    public bool enablesChecked;

    [SerializeField]
    private Material[] _ballMaterials;

    private bool _isDestroying;

    public void Init(Transform cup, Transform destroyCheck)
    {
        _cup = cup;
        _destroyLine = destroyCheck;
        if (Random.Range(0,2) != 0)
        {
            _enablesCheck = 1;
            _enables[0].SetActive(false);
        }
        else
        {
            _enablesCheck = 0;
            _enables[1].SetActive(false);
        }
        _ball.GetComponent<MeshRenderer>().material = _ballMaterials[Random.Range(0, _ballMaterials.Length)];
    }

    public void OnGoodClick()
    {
        _ball.parent = null;
        _ball.DOMoveY(_ball.position.y + 10, 0.25f).OnComplete(() => _ball.DOMove(_cup.position, 0.25f).OnComplete(() =>
        {
            Destroy(_ball.gameObject);
            PimoGameController.onBallReachCup?.Invoke();
        }));
    }

    public void DoDestroy()
    {
        _isDestroying = true;
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
    }

    public int GetEnables()
    {
        return _enablesCheck;
    }

    private void LateUpdate()
    {
        if (_isDestroying)
            return;
        transform.position += new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime;
        if (transform.position.z >= _destroyLine.position.z)
        {
            DoDestroy();
            PimoGameController.doSomthingWithHearts?.Invoke(-1);
        }
    }
}
