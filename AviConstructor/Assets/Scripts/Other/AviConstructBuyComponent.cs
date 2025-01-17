using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum BuyComponentType
{
    perDistance,
    perPrice,
}

[RequireComponent(typeof(Button))]
public class AviConstructBuyComponent : MonoBehaviour,IPointerClickHandler
{
    public BuyComponentType buyType;

    public Sprite aviSellSprite;
    public int aviSellPrice;
    public int aviConstructIndex;
    public string aviSellKey;

    [SerializeField]
    private Image _aviSellImageDisplay;

    [SerializeField]
    private TMP_Text _aviSellPriceDisplay;

    [SerializeField]
    private Image _aviSellStarImage;

    [SerializeField]
    private Image _aviBlockPanel;

    private int _aviConstructIsBuyed
    {
        get
        {
            if (PlayerPrefs.HasKey(aviSellKey + aviConstructIndex))
            {
                return PlayerPrefs.GetInt(aviSellKey + aviConstructIndex);
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt(aviSellKey + aviConstructIndex, value);
        }
    }

    private void Start()
    {
        if (buyType == BuyComponentType.perDistance)
        {
            _aviSellStarImage.gameObject.SetActive(false);
            if (AviGameComponent.aviGameBestReachedDistance >= aviSellPrice)
            {
                _aviConstructIsBuyed = 1;
            }
        }
        _aviSellImageDisplay.sprite = aviSellSprite;
        StateUpdate();
    }

    public void StateUpdate()
    {
        if (buyType == BuyComponentType.perDistance)
        {
            if (_aviConstructIsBuyed != 0)
            {
                _aviBlockPanel.gameObject.SetActive(false);
                _aviSellPriceDisplay.text = "EQUIPED";

            }
            else
            {
                _aviBlockPanel.gameObject.SetActive(true);
                _aviSellPriceDisplay.text = aviSellPrice.ToString() + "m";
            }
        }
        else
        {
            if (_aviConstructIsBuyed != 0)
            {
                _aviBlockPanel.gameObject.SetActive(false);
                _aviSellPriceDisplay.text = "EQUIPED";

            }
            else
            {
                _aviBlockPanel.gameObject.SetActive(true);
                _aviSellPriceDisplay.text = "x" + aviSellPrice.ToString();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AviBuy();
    }

    public void AviBuy()
    {
        if (AviGameComponent.aviGameStarsCount >= aviSellPrice)
        {
            AviGameComponent.aviGameStarsCount -= aviSellPrice;
            _aviConstructIsBuyed = 1;
            StateUpdate();
        }
        else
        {
            Handheld.Vibrate();
        }
    }
}
