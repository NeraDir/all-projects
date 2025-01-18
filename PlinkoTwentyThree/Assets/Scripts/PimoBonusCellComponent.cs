using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PimoBonusCellComponent : MonoBehaviour
{
    private TMP_Text _xDisplay;

    private int rndX;

    private void Start()
    {
        rndX = Random.Range(1, 30);
        _xDisplay = GetComponentInChildren<TMP_Text>();
        _xDisplay.text = "x" + rndX.ToString();
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, 0, -1) * 5 * Time.deltaTime;
    }

    public void OnTrigger()
    {
        PimoGameController._scoreCount += rndX * Random.Range(5, 15);
        transform.parent.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(transform.parent.gameObject));
    }
}
