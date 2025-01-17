using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillBallNearComponent : MonoBehaviour
{
    [SerializeField]
    private Transform _chillTarget;

    [SerializeField]
    private Sprite[] _chillSprites;

    private SpriteRenderer _chillRenderer;

    private float time;

    private bool _isClicked;

    private int _chillIndex;

    private float _chillDirection;

    private void Start()
    {
        _chillRenderer = GetComponent<SpriteRenderer>();
        _chillDirection = 1;
        _chillIndex = 0;
    }

    private void LateUpdate()
    {
        transform.RotateAround(_chillTarget.position,transform.forward, _chillDirection *120 * Time.deltaTime);
    }

    public void OnChangeDirection() 
    {
        _isClicked = !_isClicked;
        _chillDirection = _isClicked ? 1 : -1;
    }

    public void OnChillChange()
    {
        _chillIndex++;
        if (_chillIndex >= _chillSprites.Length)
        {
            _chillIndex = 0;
        }
        _chillRenderer.sprite = _chillSprites[_chillIndex];
    }
}
