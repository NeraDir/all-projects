using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBall : MonoBehaviour
{
    private Rigidbody goldBallBody;

    public int goldBallIndex;

    public bool goldBallGo;

    public float goldBallSpeed;

    public GameObject goldPrefab;

    private void Start()
    {
        goldBallBody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (goldBallGo)
            return;
        goldBallBody.velocity = new Vector3(1 * goldBallSpeed, goldBallBody.velocity.y, goldBallBody.velocity.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GoldEndWall endWall))
        {
            if (endWall.goldBallIndex == goldBallIndex)
            {
                GoldGameManagment.goldErnedballsList.Add(goldPrefab);
                GoldGameManagment.goldballsEarnedCount++;
                GoldGameManagment.goldScore += Random.Range(10, 20);
                Destroy(gameObject);
            }
            else
            {
                GoldGameManagment.goldHeartsCount--;
                Destroy(gameObject);
            }
        }
        else if (other.TryGetComponent(out GoldXFallPlace xFall))
        {
            GoldPostGameController.goldResultScore += GoldGameManagment.goldScore * xFall.goldXCount;
            goldBallBody.velocity = Vector3.zero;
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => { GoldGameManagment.goldErnedballsList.Remove(GoldGameManagment.goldErnedballsList[GoldGameManagment.goldErnedballsList.Count - 1]); Destroy(gameObject); });
        }
    }
}
