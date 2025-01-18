using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class blockComponent : MonoBehaviour, ICollisionable
{
    private TMP_Text _blockHealthTxt;

    [SerializeField]
    private textDisplayerComponent _displayTxt;

    [SerializeField]
    private GameObject _effect;

    private int _blockHealth;

    private AudioClip _clip;

    private void Start()
    {
        _blockHealth = 1;
        _blockHealth += Random.Range(1,gameController.LevelIndex + 2);
        _blockHealthTxt = GetComponentInChildren<TMP_Text>();
        _blockHealthTxt.text = _blockHealth.ToString();
        _clip = Resources.Load("Audio/block") as AudioClip;
    }

    public void Use()
    {
        transform.DOScale(transform.localScale * 1.2f, 0.1f).OnComplete(() =>
        {
            transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
            {
                int getValue = Random.Range(10, 20) * (gameController.LevelIndex + 1);
                audioManager.playSound?.Invoke(_clip);
                Instantiate(_effect, transform.position, Quaternion.identity, transform.parent);
                textDisplayerComponent text = Instantiate(_displayTxt, transform.position, Quaternion.identity, transform.parent);
                text.transform.localScale = Vector3.one;
                text.Init("+" + getValue.ToString(), () => gameController.changeScore?.Invoke(getValue));
                ballComponent.BallsChange?.Invoke(-_blockHealth);
                objectsSpawner._walls.Remove(transform.parent.gameObject);
                Destroy(transform.parent.gameObject);
                Destroy(gameObject);
            });
        });
    }
}
