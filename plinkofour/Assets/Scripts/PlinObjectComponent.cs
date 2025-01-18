using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlinObjectComponent : MonoBehaviour
{
    private Vector3 startScale;

    private void Start()
    {
        startScale = transform.localScale;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out BonusBallComponent bball))
        {
            transform.DOScale(startScale / 2, 0.25f);
        }
    }

    private void OnTriggerExit()
    {
        transform.DOScale(startScale, 0.25f);
    }
}
