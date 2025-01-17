using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeastEnergyMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _beastEnergyBestTimeLife;

    [SerializeField] private GameObject _beastEnergyHowToPlay;

    [SerializeField] private TMP_Text _beastEnergyCoinsContDisplayer;

    [SerializeField] private BeastEnergyShopManager[] _beastEnergyShopManagers;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BeastEnergyHowToPlaySave"))
        {
            _beastEnergyShopManagers[0].Buy();
            _beastEnergyHowToPlay.SetActive(true);
            PlayerPrefs.SetInt("BeastEnergyHowToPlaySave", 1);
        }
        foreach (var item in _beastEnergyShopManagers)
        {
            item.Init();
        }
    }

    private void LateUpdate()
    {
        _beastEnergyCoinsContDisplayer.text = BeastEnergyGameManager.beastEnergyCoinsCount.ToString("0");
        _beastEnergyBestTimeLife.text = BeastEnergyGameManager.beastEnergyRecordLiveTime.ToString("0") + "s";
    }

    public void ClickPlay() 
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ClickQuit() 
    {
        Application.Quit();
    }
}
