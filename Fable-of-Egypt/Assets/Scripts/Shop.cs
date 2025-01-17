using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Shop : MonoBehaviour
{
    [SerializeField] private List<int> priceCatapulta = new List<int>();
    [SerializeField] private List<int> priceHealth = new List<int>();

    [SerializeField] private List<bool> isBuyCatapult = new List<bool>();
    [SerializeField] private List<bool> isBuyHearth = new List<bool>();

    [SerializeField] private List<Button> btnLevels = new List<Button>();
    [SerializeField] private List<Button> btnCatapulta = new List<Button>();
    [SerializeField] private List<Button> btnHeart = new List<Button>();
    [SerializeField] private List<GameObject> obgImagesLevel = new List<GameObject>();

    private void Start()
    {
       //PlayerPrefs.DeleteAll();
        LoadData();
    }

    public void BuyCatapult(int id)
    {
        if (isBuyCatapult[id]) return;

        if (Wallet.instance.SubstrictCoin(priceCatapulta[id]))
        {
            // buy
            isBuyCatapult[id] = true;

            RefreshButtons();
            SaveData();
        }
    }

    public void BuyHearth(int id)
    {
        if (isBuyHearth[id]) return;

        if (Wallet.instance.SubstrictCoin(priceHealth[id]))
        {
            // buy
            isBuyHearth[id] = true;

            Wallet.instance.SetHeart(id);

            RefreshButtons();
            SaveData();
        }
    }

    private void RefreshButtons()
    {
        for (int i = 0; i < btnCatapulta.Count; i++)
        {
            btnCatapulta[i].interactable = !isBuyCatapult[i];
            btnLevels[i].interactable = isBuyCatapult[i];
            obgImagesLevel[i].SetActive(!isBuyCatapult[i]);
        }

        for (int i = 0; i < btnHeart.Count; i++)
        {
            btnHeart[i].interactable = !isBuyHearth[i];
        }
    }

    private void LoadData()
    {
        for (int i = 1; i < btnCatapulta.Count; i++)
        {
            isBuyCatapult[i] = Convert.ToBoolean(PlayerPrefs.GetInt($"isBuyCatapult{i}", 0));
        }

        for (int i = 1; i < btnHeart.Count; i++)
        {
            isBuyHearth[i] = Convert.ToBoolean(PlayerPrefs.GetInt($"isBuyHearth{i}", 0));
        }

        RefreshButtons();
    }

    public void SaveData()
    {
        for (int i = 1; i < btnCatapulta.Count; i++)
        {
            PlayerPrefs.SetInt($"isBuyCatapult{i}", Convert.ToInt32(isBuyCatapult[i])); //  = Convert.ToBoolean(PlayerPrefs.GetInt($"isBuyCatapult{i}", 0));
        }

        for (int i = 1; i < btnHeart.Count; i++)
        {
            PlayerPrefs.SetInt($"isBuyHearth{i}", Convert.ToInt32(isBuyHearth[i]));
        }

        PlayerPrefs.Save();
    }

}
