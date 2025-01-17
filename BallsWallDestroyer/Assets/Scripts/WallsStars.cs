using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsStars : MonoBehaviour
{
    private Vector3 beginStarScale;

    private void Start()
    {
        beginStarScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(beginStarScale, 0.25f);
    }

    public void GetMe() 
    {
        transform.DOScale(Vector3.zero, 0.1f).OnComplete(() => OnComplete());
    }

    private void OnComplete() 
    {
        GameController.earnedStars += 1;
        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 1, 0), 360 * Time.deltaTime);
    }
}
