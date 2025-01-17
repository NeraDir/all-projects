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
    [SerializeField] private int[] _slotsPrice;

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
        for (int i = 0; i < _textsSlots.Length; i++)
        {
            if (PrefsControl.HaveSkeen(i))
                _textsSlots[i].text = "use";
            else
                _textsSlots[i].text = _slotsPrice[i].ToString() + " gold";
        }
        _textsSlots[PrefsControl.GetSceenNum()].text = "used";

        GoMain();
    }
    public void BuySkeen(int num)
    {

        _textsSlots[PrefsControl.GetSceenNum()].text = "use";
        if (PrefsControl.TrySetSkeen(num))
        {
            _textsSlots[PrefsControl.GetSceenNum()].text = "used";
            return;
        }
        _textsSlots[PrefsControl.GetSceenNum()].text = "used";
        if (PrefsControl.GetGoald() >= _slotsPrice[num])
        {
            PrefsControl.ChageGoald(-_slotsPrice[num]);

            _textsSlots[PrefsControl.GetSceenNum()].text = "use";
            PrefsControl.BuySceen(num);
            _textsSlots[PrefsControl.GetSceenNum()].text = "used";
        }
        
        _goldText.text = PrefsControl.GetGoald().ToString();
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
