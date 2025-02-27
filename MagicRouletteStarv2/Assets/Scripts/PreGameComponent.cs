using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreGameComponent : MonoBehaviour
{
    public static CrystallType targetSprite;

    [SerializeField] private Image _targetImage;

    [SerializeField] private Sprite[] _crystallSprites;

    private void Start()
    {
        targetSprite = default;
    }

    public void OnDemostrate()
    {
        _targetImage.sprite = _crystallSprites[(int)targetSprite];
    }

    public void OnEnd()
    {
        GameComponent.onLaunchGame?.Invoke(targetSprite);
        Debug.Log(targetSprite.ToString());
        gameObject.SetActive(false);
    }
}
