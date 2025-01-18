using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class menuScreenController : MonoBehaviour
{
    public static int userDiamondsCount
    {
        get => PlayerPrefs.HasKey("MysteriousCircuitsUserDiamondsCountSaveKey") ? PlayerPrefs.GetInt("MysteriousCircuitsUserDiamondsCountSaveKey") : 0;
        set => PlayerPrefs.SetInt("MysteriousCircuitsUserDiamondsCountSaveKey", value);
    }

    public static int userMaxScore
    {
        get => PlayerPrefs.HasKey("MysteriousCircuitsUserMaxScoreSaveKey") ? PlayerPrefs.GetInt("MysteriousCircuitsUserMaxScoreSaveKey") : 0;
        set => PlayerPrefs.SetInt("MysteriousCircuitsUserMaxScoreSaveKey", value);
    }

    public static Action updateDiamondCount;

    [SerializeField]
    private GameObject _infoScreen;

    [SerializeField]
    private GameObject _menuScreen;

    [SerializeField]
    private TMP_Text _maxScoreText;

    private void Awake()
    {
        updateDiamondCount?.Invoke();
        _maxScoreText.text = userMaxScore.ToString();
        if (!PlayerPrefs.HasKey("MysteriousCiruitsInfoScreenDispalyedKey"))
        {
            _infoScreen.SetActive(true);
            _menuScreen.SetActive(false);
            PlayerPrefs.SetInt("MysteriousCiruitsInfoScreenDispalyedKey", 1);
        }
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
