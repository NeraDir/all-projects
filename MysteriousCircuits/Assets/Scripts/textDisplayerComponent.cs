using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textDisplayerComponent : MonoBehaviour
{
    public void Init(string txt, Action action)
    {
        TMP_Text text = GetComponent<TMP_Text>();
        text.text = txt;
        transform.DOMoveY(transform.position.y + 1f, 0.4f);
        text.DOFade(0, 0.4f).OnComplete(() =>
        {
            action?.Invoke();
            Destroy(gameObject);
        });
    }
}
