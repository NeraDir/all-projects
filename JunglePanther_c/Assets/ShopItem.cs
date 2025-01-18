using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int index;
    [SerializeField]
    private int price;
    [SerializeField]
    private bool isBuyed;

    [SerializeField]
    private TMP_Text priceDisplayTXT;

    [SerializeField]
    private List<GameObject> pantherIcon;


    [SerializeField]
    private GameObject pricePanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isBuyed)
        {
            if (MenuSceneController.coinCount - price >= 0)
            {
                PantherRunnerData.modelIndex = index;

                MenuSceneController.coinCount -= price;

                pantherIcon[0].SetActive(false);
                pantherIcon[1].SetActive(true);
                pricePanel.SetActive(false);
                isBuyed = true;
            }
        }
    }

    private void OnEnable()
    {
        if (index <= PantherRunnerData.modelIndex)
        {
            pantherIcon[0].SetActive(false);
            pantherIcon[1].SetActive(true);
            pricePanel.SetActive(false);
            isBuyed = true;
        }
        else
        {
            pantherIcon[0].SetActive(true);
            pantherIcon[1].SetActive(false);
            pricePanel.SetActive(true);
            priceDisplayTXT.text = price.ToString();
            isBuyed = false;
        }
    }
}
