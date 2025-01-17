using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDefenceShieldMan : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _shieldManSprites;

    private int _shieldSpriteIndex;

    [SerializeField]
    private SpriteRenderer _shieldRenderer;

    private void Start()
    {
        _shieldSpriteIndex = Random.Range(0, _shieldManSprites.Length);
        _shieldRenderer.sprite = _shieldManSprites[_shieldSpriteIndex];
    }

    private void OnMouseDown()
    {
        _shieldSpriteIndex++;
        if (_shieldSpriteIndex >= _shieldManSprites.Length)
        {
            _shieldSpriteIndex = 0;
        }
        _shieldRenderer.sprite = _shieldManSprites[_shieldSpriteIndex];
    }
}
