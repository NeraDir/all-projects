using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastEnergyRoadManager : MonoBehaviour
{
    private void Start()
    {
        Vector3 startScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(startScale, 0.25f);
    }
}
