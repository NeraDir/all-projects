using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getball : MonoBehaviour
{
    private Image _ballimage;

    [SerializeField]
    private Sprite[] _ballSprites;

    private void Start()
    {
        _ballimage = GetComponent<Image>();
        _ballimage.sprite = _ballSprites[Random.Range(0,_ballSprites.Length)];
    }
}
