using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Bullet BulletPrefab;

    public Image PlayerImage;

    public List<skinInfo> skinInfos = new();
    public List<Transform> positions = new();
    private int Line = 1;

    private void Start()
    {
        if (ShopManager.SkinNum == -1)
            PlayerImage.sprite = skinInfos[0].sprite;
        else
            PlayerImage.sprite = skinInfos[ShopManager.SkinNum].sprite;
    }

    private void Update()
    {
        if (!CelestialGameManager.Instance.GameStarted) return;

        if (MobileInput.instance.Tap)
        {
            Fire();
        }

        if (MobileInput.instance.SwipeLeft)
        {
            Line--;

            if (Line == -1)
                Line = 2;

            if (ShopManager.SkinNum == -1)
                PlayerImage.sprite = skinInfos[0].ColoredSprites[Line];
            else
                PlayerImage.sprite = skinInfos[ShopManager.SkinNum].ColoredSprites[Line];

            transform.DOMoveX(positions[Line].position.x, 1f);
        }

        if (MobileInput.instance.SwipeRight)
        {
            Line++;

            if (Line == 3)
                Line = 0;

            if (ShopManager.SkinNum == -1)
                PlayerImage.sprite = skinInfos[0].ColoredSprites[Line];
            else
                PlayerImage.sprite = skinInfos[ShopManager.SkinNum].ColoredSprites[Line];

            transform.DOMoveX(positions[Line].position.x, 1f);
        }
    }

    private void Fire()
    {
        var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity, CelestialGameManager.Instance.Parrent);

        if (ShopManager.SkinNum == -1)
            bullet.INIT(skinInfos[0].ColoredSprites[Line], Line);
        else
            bullet.INIT(skinInfos[ShopManager.SkinNum].ColoredSprites[Line], Line);
    }
}

[System.Serializable]
public struct skinInfo
{
    public Sprite sprite;
    public List<Sprite> ColoredSprites;
}