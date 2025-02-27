using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BettyShopComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text _stateText;

    [SerializeField] private int _price;
    [SerializeField] private int _index;

    private AudioClip _goodClip;
    private AudioClip _badClip;

    private void Awake()
    {
        _badClip = Resources.Load("Sound/error") as AudioClip;
        _goodClip = Resources.Load("Sound/success") as AudioClip;
        VisualUpdate();
    }

    public void Buy()
    {
        if (ProfileData.BettysPlayerSkinsBoughtList.Contains(_index))
            return;
        if (ProfileData.BettysPlayerCoins >= _price)
        {
            BettersMusicComponent.instance.playSound?.Invoke(_goodClip);
            ProfileData.BettysPlayerCoins -= _price;
            ProfileData.AddSkin(_index);
            
            VisualUpdate();
        }
        else
        {
            BettersMusicComponent.instance.playSound?.Invoke(_badClip);
        }
    }

    private void VisualUpdate()
    {
        if (ProfileData.BettysPlayerSkinsBoughtList.Contains(_index))
        {
            _stateText.text = "BOUGHT";
        }
        else
        {
            _stateText.text = "x" + _price.ToString();
        }
    }
}
