using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameInfoScreen;

    [SerializeField]
    private Text _bestReachedDistance;

    [SerializeField]
    private ShopManager[] _shopManagers;

    [SerializeField]
    private Text _coinsShow;

    public static float BestReachedDistance 
    {
        get
        {
            if (PlayerPrefs.HasKey("BeastPowerBestDistanceSaveKey"))
            {
                return PlayerPrefs.GetFloat("BeastPowerBestDistanceSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetFloat("BeastPowerBestDistanceSaveKey", value);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BeastPowerGameInfoSeeSaveKey"))
        {
            _gameInfoScreen.SetActive(true);
            _shopManagers[0].OnClickBuy();
            UpdateAllShops();
            PlayerPrefs.SetInt("BeastPowerGameInfoSeeSaveKey", 1);
        }
        _bestReachedDistance.text = BestReachedDistance.ToString("0.0") + "m";
    }

    private void LateUpdate()
    {
        _coinsShow.text = "x" + GameManager.Coins.ToString();
    }

    public void UpdateAllShops() 
    {
        foreach (var item in _shopManagers)
        {
            item.UpdateStatus();
        }
    }

    public void OnClickPlay() 
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickExit() 
    {
        Application.Quit();
    }
}
