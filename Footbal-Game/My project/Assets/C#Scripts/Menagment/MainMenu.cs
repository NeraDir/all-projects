using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private GameObject _menuClose;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _lvls;
    [SerializeField] private Transform _lvlsConteiner;
    [SerializeField] private int[] _skeensPrice;
    [SerializeField] private GameObject _howToPlay;
    private void Awake()
    {
        Menu();
        if (!PlayerPrefs.HasKey("gold"))
        {
            PlayerPrefs.SetInt("gold", 0);
            PlayerPrefs.SetInt("skeen", 0);
            PlayerPrefs.SetInt("lvlsOpend", 1);
            _howToPlay.SetActive(true);
            return;
        }
        for(int i = 0; i < _skeensPrice.Length; i++)
        {
            if(PlayerPrefs.HasKey("skeen_" + i.ToString()))
            {
                _shop.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "use";
            }
            else
            {
                _shop.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = _skeensPrice[i].ToString();
            }
        }
        _shop.transform.GetChild(PlayerPrefs.GetInt("skeen")).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "used";
        _goldText.text = PlayerPrefs.GetInt("gold").ToString();
        for (int i = 0; i < _lvlsConteiner.childCount; i++)
        {
            if( i < PlayerPrefs.GetInt("lvlsOpend"))
            {
                _lvlsConteiner.GetChild(i).GetComponent<Image>().color = Color.white;
            }
            else
            {
                _lvlsConteiner.GetChild(i).GetComponent<Image>().color = new Color( 0.5f, 0.5f, 0.5f, 1 );
            }
        }
    }
    public void TryBuySkeen(int num)
    {
        if(PlayerPrefs.GetInt("gold") < _skeensPrice[num])
        {
            return;
        }
        else if(PlayerPrefs.HasKey("skeen_" + num.ToString()))
        {
            ChengeSkeen(num);
        }
        else
        {
            BuySkeen(num);
        }
    }
    private void BuySkeen(int num)
    {
        PlayerPrefs.SetInt("skeen_" + num.ToString(), num);
        PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") - _skeensPrice[num]);
        _goldText.text = PlayerPrefs.GetInt("gold").ToString();
        ChengeSkeen(num);
    }
    private void ChengeSkeen(int num)
    {
        _shop.transform.GetChild(PlayerPrefs.GetInt("skeen")).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "use";
        
        PlayerPrefs.SetInt("skeen", num);
        _shop.transform.GetChild(num).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "used";
    }
    public void StartLevel(int num)
    {
        if (num >= PlayerPrefs.GetInt("lvlsOpend"))
        {
            return;
        }
        SceneManager.LoadScene("Level_" + (num + 1).ToString());
    }
    public void Shop() 
    {
        _menu.SetActive(false);
        _shop.SetActive(true);
        _menuClose.SetActive(true);
    }
    public void Menu() 
    {
        _menuClose.SetActive(false);
        _lvls.SetActive(false);
        _shop.SetActive(false);
        _menu.SetActive(true);

    }
    public void Lvls()
    {
        _menu.SetActive(false);
        _lvls.SetActive(true);
        _menuClose.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    } 
}
