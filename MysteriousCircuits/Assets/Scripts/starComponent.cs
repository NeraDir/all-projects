using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class starComponent : MonoBehaviour, ICollisionable
{
    [SerializeField]
    private textDisplayerComponent _displayTxt;

    [SerializeField]
    private GameObject _effect;

    private AudioClip _clip;

    private void Awake()
    {
        _clip = Resources.Load("Audio/star") as AudioClip;
    }

    public void Use()
    {
        transform.DOScale(transform.localScale * 1.2f, 0.1f).OnComplete(() =>
        {
            transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
            {
                int getValue = Random.Range(1, 5) * (gameController.LevelIndex + 1);
                audioManager.playSound?.Invoke(_clip);
                Instantiate(_effect, transform.position, Quaternion.identity, transform.parent);
                textDisplayerComponent text = Instantiate(_displayTxt,transform.position,Quaternion.identity,transform.parent);
                text.transform.localScale = Vector3.one;
                text.Init("+" + getValue.ToString(), () => gameController.changeScore?.Invoke(getValue));
                Destroy(gameObject);
            });
        });
    }
}
