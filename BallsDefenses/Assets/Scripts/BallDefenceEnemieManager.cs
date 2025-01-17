using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallDefenceEnemieManager : MonoBehaviour
{
    public bool IsLast;

    [SerializeField]
    private Sprite[] _enemiesSprites;

    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private TMP_Text _starsAddShow;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _enemiesSprites[Random.Range(0,_enemiesSprites.Length)];
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(-1, 0, 0) * BallDefenceGameController.EnemiesMoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out BallDefenceShieldMan shieldMan))
        {
            if (shieldMan.GetComponentInChildren<SpriteRenderer>().sprite == _spriteRenderer.sprite)
            {
                transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
                int addStars = Random.Range(1, 2);
                TMP_Text tempTxt = Instantiate(_starsAddShow, transform.position, Quaternion.identity);
                BallDefenceGameController.StarsCount += addStars;
                tempTxt.text = "x" + addStars.ToString();
            }
            else
            {
                shieldMan.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(shieldMan.gameObject));
            }
        }
        if (collision.TryGetComponent(out BallDefenceKingManager king))
        {
            king.hearts -= 1;
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
        }
    }

    private void OnDestroy()
    {
        if (IsLast)
        {
            BallDefenceGameController.WaveIsEnde = true;
        }
    }
}
