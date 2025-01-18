using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgAnimationComponent : MonoBehaviour
{
    private List<float> _yValuel = new List<float>();

    [SerializeField]
    private Transform[] _bgElements;

    private void Start()
    {
        foreach (var item in _bgElements)
        {
            _yValuel.Add(Random.Range(-31.7f, 0));
        }
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < _bgElements.Length; i++)
        {
            sequence.Append(_bgElements[i].DOMoveY(_yValuel[i], 1));
            sequence.Append(_bgElements[i].DOMoveY(0, 1));
        }
        sequence.SetLoops(-1, LoopType.Yoyo);
    }
}
