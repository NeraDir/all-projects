using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ballComponent : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _ballSprites;

    [SerializeField]
    private TMP_Text _ballsInMeTxt;

    private int _ballsInMe;

    private Image _ballImage;

    public static Action<int> BallsChange;
    public static Action GetBalls;

    private AudioClip _clip;

    public static int BallSpriteIndex
    {
        get => PlayerPrefs.HasKey("MysteriousCircuitsBallSpriteIndexKey") ? PlayerPrefs.GetInt("MysteriousCircuitsBallSpriteIndexKey") : 0;
        set => PlayerPrefs.SetInt("MysteriousCircuitsBallSpriteIndexKey",value);
    }

    private void Start()
    {
        _clip = Resources.Load("Audio/move") as AudioClip;
        _ballsInMe = 5;
        for (int i = 0; i < (gameController.LevelIndex + 1); i++)
        {
            _ballsInMe += 2;
        }
        _ballImage = GetComponentsInChildren<Image>()[1];
        _ballImage.sprite = _ballSprites[BallSpriteIndex];
        BallsChange += OnBallsCountChanged;
        _ballsInMeTxt.text = _ballsInMe.ToString();
        userInputImageComponent.sendTapPosition += MoveToPosition;
    }

    private void OnDestroy()
    {
        BallsChange -= OnBallsCountChanged;
        userInputImageComponent.sendTapPosition -= MoveToPosition;
    }

    private void MoveToPosition(Vector3 position)
    {
        audioManager.playSound?.Invoke(_clip);
        BallsChange?.Invoke(-1);
        transform.DOLocalMove(position, 0.25f);
    }

    public int GetBallsInMe()
    {
        return _ballsInMe;
    }

    private void OnBallsCountChanged(int value)
    {
        _ballsInMe += value;
        _ballsInMeTxt.text = _ballsInMe.ToString();
        if (_ballsInMe <= 0)
            gameController.getResult?.Invoke(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ICollisionable collide))
        {
            collide.Use();
        }
    }
}
