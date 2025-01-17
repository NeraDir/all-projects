using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChillGameOtherBalls : MonoBehaviour
{
    private Transform _target;

    [SerializeField]
    private Sprite[] _chillOtherBalls;

    private SpriteRenderer _renderer;

    [SerializeField]
    private TMP_Text _chillScoreTxt;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _target = FindObjectOfType<ChillGameBallController>().transform;
        _renderer.sprite = _chillOtherBalls[Random.Range(0,_chillOtherBalls.Length)];
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, ChillBaseBallGameController.chillOtherBallsMove * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ChillBallNearComponent nearBall))
        {
            if (nearBall.GetComponent<SpriteRenderer>().sprite == GetComponent<SpriteRenderer>().sprite)
            {
                transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
                TMP_Text tempTxt = Instantiate(_chillScoreTxt, transform.position, Quaternion.identity);
                int tempScore = Random.Range(1, 5);
                ChillBaseBallGameController.chillScore += tempScore;
                tempTxt.text = "+" + tempScore.ToString("0");
            }
            else
            {
                ChillBaseBallGameController.chillHearts--;
                transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
            }
        }
        if (other.TryGetComponent(out ChillGameBallController ball))
        {
            ChillBaseBallGameController.chillHearts--;
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
        }
    }
}
