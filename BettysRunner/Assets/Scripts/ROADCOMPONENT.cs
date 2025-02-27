using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROADCOMPONENT : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DOMoveY(-1.76f, 0.5f);
    }
}
