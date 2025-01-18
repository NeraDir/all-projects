using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class RockUpComponent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _destroyEffect;

    [SerializeField] private bool _doSomthing;
    private bool _isDestroyble;
    private bool _isMoveble;
    
    private Vector3 _startPosition;
    
    private void Awake()
    {
        _text.text = "";
        if(_doSomthing)
            return;
        _isDestroyble = Random.Range(0,2) != 0 ? true : false;
        _isMoveble = _isDestroyble ? false : Random.Range(0,2) != 0 ? true : false;
        _startPosition = transform.position;
        if (_isMoveble)
            Moveable();
    }

    private void Moveable()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveX(_startPosition.x + Random.Range(0.5f,2f), Random.Range(5,10)));
        sequence.Append(transform.DOMoveX(_startPosition.x - Random.Range(0.5f,2f), Random.Range(5,10)));
        sequence.Append(transform.DOMoveX(_startPosition.x, Random.Range(5,10)));
        sequence.SetLoops(-1, LoopType.Restart);
    }
    
    private void OnMouseDown()
    {
        if(!GameManager.gameLaunched)
            return;
        if (_isDestroyble)
            TigetManager.moveTheTiger?.Invoke(_target,OnComplete);
        else
            TigetManager.moveTheTiger?.Invoke(_target,null);
    }

    private void OnComplete()
    {
        StartCoroutine(WaitToDestroy());
    }

    private IEnumerator WaitToDestroy()
    {
        _text.text = "2";
        float currentTime = 0;
        while (currentTime < 2)
        {
            currentTime += Time.deltaTime;
            _text.text = currentTime.ToString("0");
            yield return null;
        }

        if (_isDestroyble)
        {
            Destroy(gameObject);
            Instantiate(_destroyEffect,transform.position,Quaternion.identity);
        }
    }
}
