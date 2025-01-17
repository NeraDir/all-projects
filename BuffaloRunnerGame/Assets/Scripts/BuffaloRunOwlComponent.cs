using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffaloRunOwlComponent : MonoBehaviour
{
    public bool buffaloOnTheGround;

    [SerializeField]
    private LayerMask _buffaloGroundLayer;

    private Vector3 _buffaloMaxScale = new Vector3(7.301528f, 7.301528f, 7.301528f);
    private Vector3 _buffaloMinScale = new Vector3(3.106654f, 3.106654f, 3.106654f);

    private Vector3 _buffaloIncrementScale = new Vector3(0.25f, 0.25f, 0.25f);

    public static bool isStop;

    private void LateUpdate()
    {
        CheckBuffaloScale();
        if (isStop)
            return;
        buffaloOnTheGround = Physics.CheckSphere(transform.position, 0.5f, _buffaloGroundLayer);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BuffaloRunGameTrapsComponent traps))
        {
            transform.DOScale(transform.localScale - _buffaloIncrementScale, 0.1f);
            traps.transform.DOScale(Vector3.zero, 0.1f).OnComplete(() => { Destroy(traps.gameObject); });
        }
        if (other.TryGetComponent(out BufffaloHayComponent hay))
        {
            transform.DOScale(transform.localScale + _buffaloIncrementScale, 0.1f);
            hay.transform.DOScale(Vector3.zero, 0.1f).OnComplete(() => { Destroy(hay.gameObject); });
        }
        if (other.TryGetComponent(out BuffaloRunCoinComponent coin))
        {
            coin.transform.DOScale(Vector3.zero, 0.1f).OnComplete(() => { Destroy(coin.gameObject); BuffaloRunGameController.currentScore += Random.Range(10, 30);BuffaloRunGameController.currentStars += 1; });
        }
        if (other.TryGetComponent(out BuffaloWallComponent wall))
        {
            transform.DOScale(transform.localScale - _buffaloIncrementScale, 0.1f);
            wall.transform.DOScale(Vector3.zero, 0.1f).OnComplete(() => { Destroy(wall.gameObject); BuffaloRunGameController.xValue = wall.xValue; });
        }
    }

    private void CheckBuffaloScale()
    {
        if (transform.localScale.x <= 3.106654f)
        {
            isStop = true;
        }
        else
        {
            isStop = false;
        }
    }
}
