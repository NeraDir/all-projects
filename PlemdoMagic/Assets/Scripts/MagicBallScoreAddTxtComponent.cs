using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagicBallScoreAddTxtComponent : MonoBehaviour
{
    private TMP_Text txt;

    public int value;

    private void Start()
    {
        txt = GetComponent<TMP_Text>();
        txt.text = value.ToString();
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        transform.DOMove(pos, 1).OnComplete(() => transform.DOScale(Vector3.zero, 1).OnComplete(() => Destroy(gameObject)));
    }
}
