using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI[] _textsSlots;
    [SerializeField] private int[] _upPrice;

    [SerializeField] private Image[] _UpImage1;
    [SerializeField] private Image[] _UpImage2;
    [SerializeField] private Image[] _UpImage3;

    [SerializeField] private GameObject _main;
    [SerializeField] private GameObject _play;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _exitButton;

    [SerializeField] private int _levelsNum;
    [SerializeField] private GameObject _levelPrefab;
    [SerializeField] private Transform _levelsPrefabContainer;

    public static MainMenu instance;
    private void Awake()
    {
        instance = this;
        PrefsControl.LoadGame();
    }
    private void Start()
    {
        for (int i = 0; i < _levelsNum; i++)
        {
            GameObject g = Instantiate(_levelPrefab, _levelsPrefabContainer);
            g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "lvl " + (i + 1).ToString();
            g.GetComponent<ButtonLvl>().num = i;
            if (i >= PrefsControl.GetMaksOpenedLvlNum())
                g.transform.GetComponent<Image>().color = Color.gray;
        }

        _goldText.text = PrefsControl.GetGoald().ToString();

        GoMain();
        RenderUpgrades();
    }
    private void RenderUpgrades()
    {
        for (int i = 0; i < _UpImage1.Length; i++)
        {
            if (i >= PrefsControl.GetUpgrade(0))
                _UpImage1[i].color = Color.grey;
            else
                _UpImage1[i].color = Color.white;

            if (i >= PrefsControl.GetUpgrade(1))
                _UpImage2[i].color = Color.grey;
            else
                _UpImage2[i].color = Color.white;

            if (i >= PrefsControl.GetUpgrade(2))

                _UpImage3[i].color = Color.grey;
            else
                _UpImage3[i].color = Color.white;
        }
        _textsSlots[0].text = _upPrice[PrefsControl.GetUpgrade(0)].ToString() + " gold";
        _textsSlots[1].text = _upPrice[PrefsControl.GetUpgrade(1)].ToString() + " gold";
        _textsSlots[2].text = _upPrice[PrefsControl.GetUpgrade(2)].ToString() + " gold";
    }
    public void BuyUpgade(int num)
    {
        if (PrefsControl.GetGoald() >= _upPrice[PrefsControl.GetUpgrade(num)])
        {
            PrefsControl.ChageGoald(-_upPrice[PrefsControl.GetUpgrade(num)]);
            PrefsControl.BuyUpgade(num);
            _goldText.text = PrefsControl.GetGoald().ToString();
        }
        RenderUpgrades();
    }
    public void GoShop()
    {
        _exitButton.SetActive(true);
        _shop.SetActive(true);
        _play.SetActive(false);
        _main.SetActive(false);
    }
    public void GoPlay()
    {
        _exitButton.SetActive(true);
        _shop.SetActive(false);
        _play.SetActive(true);
        _main.SetActive(false);
    }
    public void GoMain()
    {
        _exitButton.SetActive(false);
        _shop.SetActive(false);
        _play.SetActive(false);
        _main.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void StartLvl(int num)
    {
        if (PrefsControl.TryLoadLvl(num))
        {
            Debug.Log("load_lvl" + num.ToString());
            SceneManager.LoadScene("SampleScene");
        }
    }
}
