using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CaramelTreatsMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _aboutInfoScreen;
    [SerializeField] private TMP_Text _maxLevelShow;
    [SerializeField] private TMP_Text _earnedStarsShow;
    [SerializeField] private BgShopComponent[] _bgShops;
    [SerializeField] private GameObject _shopScreen;

    public static UnityEvent bgComponentIsTrigger = new UnityEvent();
    private void Start()
    {
        _shopScreen.SetActive(false);
        bgComponentIsTrigger.AddListener(OnTrigger);
        if (!PlayerPrefs.HasKey("CaramelTreath_aboutInfoShowedKey"))
        {
            _bgShops[0].isOpen = 1;
            _aboutInfoScreen.SetActive(true);
            PlayerPrefs.SetInt("CaramelTreath_aboutInfoShowedKey", 1);
        }
        _earnedStarsShow.text = "x" + CaramelTreatsGameController.EarnedStarsCount.ToString();
        _maxLevelShow.text = CaramelTreatsGameController.MaxReachLevel.ToString();
    }

    private void OnDestroy()
    {
        bgComponentIsTrigger.RemoveListener(OnTrigger);
    }

    private void OnTrigger()
    {
        foreach (var item in _bgShops)
        {
            item.UpdateVisual();
        }
        _earnedStarsShow.text = "x" + CaramelTreatsGameController.EarnedStarsCount.ToString();
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
