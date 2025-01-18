using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _starEffect;

    [SerializeField]
    private AudioClip _clip;

    private void Start()
    {
        if (Random.Range(0, 2) != 0)
        {
            Destroy(gameObject);
        }

        Vector3 tempScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(tempScale, 0.25f);
    }

    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1), 180 * Time.deltaTime);
    }

    public void OnColliseion()
    {
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
        {
            Instantiate(_starEffect, transform.position, Quaternion.identity);
            GameSavesManager.GameCurrentScoreValue += Random.Range(5, 20);
            Destroy(gameObject);
            GameManager.soundEffectSource.PlayOneShot(_clip);
        });
    }
}
