using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReactiveJatpackBuyContainer : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private TMP_Text jetpackPriceShow;

    [SerializeField]
    private GameObject jetpackSelectedImage;

    public float jetpackPrice;
    public int jetpackSelectIndex;

    public ReactiveJatpackBuyContainer[] reactivesJatpackes;

    private int jatpackBuyState 
    {
        get 
        {
            if (PlayerPrefs.HasKey("JatpackBuyStateSave" + jetpackSelectIndex))
            {
                return PlayerPrefs.GetInt("JatpackBuyStateSave" + jetpackSelectIndex);
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("JatpackBuyStateSave" + jetpackSelectIndex, value);
        }
    }

    private void Start()
    {
        CheckJatpack();
    }

    public void CheckJatpack() 
    {
        if (jatpackBuyState != 0)
        {
            jetpackPriceShow.text = "";
            if (CharacterState.jatpackSelectedIndex != jetpackSelectIndex)
            {
                jetpackSelectedImage.SetActive(false);
            }
            else
            {
                jetpackSelectedImage.SetActive(true);
            }
        }
        else
        {
            jetpackPriceShow.text = jetpackPrice.ToString();
        }


    }

    public void BuyAndSelectJatpack() 
    {
        if (jatpackBuyState != 0)
        {
            CharacterState.jatpackSelectedIndex = jetpackSelectIndex;
            foreach (var item in reactivesJatpackes)
            {
                item.CheckJatpack();
            }
        }
        else
        {
            if (AviationDataSaveClass.AviationLoveMoneys >= jetpackPrice)
            {
                AviationDataSaveClass.AviationLoveMoneys -= jetpackPrice;
                jatpackBuyState = 1;
                foreach (var item in reactivesJatpackes)
                {
                    item.CheckJatpack();
                }
            }
            else
            {
                Handheld.Vibrate();
            }
        }
        CheckJatpack();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BuyAndSelectJatpack();
    }
}
