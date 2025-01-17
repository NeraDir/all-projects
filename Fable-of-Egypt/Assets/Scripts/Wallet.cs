using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI textMoney;
    [SerializeField] private Transform posHeart;
    [SerializeField] private GameObject prefabHeart;

    [SerializeField] private int money;
    [SerializeField] private int startHealth;

    public static Wallet instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadInf();
        ReloadWallet();
    }

    public void ReloadWallet()
    {
        currentHealth = startHealth;
        ShowInf();
    }

    public int GetCion() => money;
    public void AddCoin(int value)
    {
        money += value;
        ShowInf();
    }
    public bool SubstrictCoin(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            ReloadWallet();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetHeart(int id)
    {
        startHealth = id + 1;
        ReloadWallet();
    }

    [SerializeField] private int currentHealth;
    public bool SubstractHealth()
    {
        currentHealth--;
        ShowInf();

        if (currentHealth <= 0) return false;
        else return true;
    }

    private void ShowInf()
    {
        textMoney.text = money.ToString();
        //textHealth.text = "HP: " + currentHealth.ToString();

        foreach (Transform item in posHeart.transform)
        {
            GameObject.Destroy(item.gameObject);
        }

        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(prefabHeart, posHeart);
        }

        SaveInf();
    }

    private void SaveInf()
    {
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("hp", startHealth);
        PlayerPrefs.Save();
    }

    private void LoadInf()
    {
        money = PlayerPrefs.GetInt("money", 0);
        startHealth = PlayerPrefs.GetInt("hp", 1);
    }

}
