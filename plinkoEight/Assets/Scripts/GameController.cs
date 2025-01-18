using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static int punkCrystallsTryCount
    {
        get
        {
            if (PlayerPrefs.HasKey("punkCrystallsTryCountSfgisigufdusuhgdsaves"))
            {
                return PlayerPrefs.GetInt("punkCrystallsTryCountSfgisigufdusuhgdsaves");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("punkCrystallsTryCountSfgisigufdusuhgdsaves", value);
        }
    }

    public static string punkCrystallName;

    public static int punkCrystallsWinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("punkCrystallsWinsCountsdguudyuysygysdfygsdSave"))
            {
                return PlayerPrefs.GetInt("punkCrystallsWinsCountsdguudyuysygysdfygsdSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("punkCrystallsWinsCountsdguudyuysygysdfygsdSave", value);
        }
    }

    public static int punkCrystallCurrentLevelIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("punkCrystallCurrentLevelIndexfdgusufgidfsSave"))
            {
                return PlayerPrefs.GetInt("punkCrystallCurrentLevelIndexfdgusufgidfsSave");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("punkCrystallCurrentLevelIndexfdgusufgidfsSave", value);
        }
    }

    [SerializeField]
    private TMP_Text _showCurrentLevel;

    [SerializeField]
    private GameObject _restartButton;

    [SerializeField]
    private GameObject _resultpage;

    [SerializeField]
    private GameObject[] _levelPrefabs;

    [SerializeField]
    private TMP_Text _resultTextDisplay;

    [SerializeField]
    private Transform _ballTransform;

    [SerializeField]
    private Transform _cupTransform;

    public static UnityEvent<bool> onShowResult = new UnityEvent<bool>();

    private void Start()
    {
        onShowResult.AddListener(OnResult);
        _levelPrefabs[punkCrystallCurrentLevelIndex-1].SetActive(true);
        foreach (var item in _levelPrefabs[punkCrystallCurrentLevelIndex - 1].GetComponentsInChildren<Transform>())
        {
            if (item.tag == "Respawn")
            {
                _ballTransform.position = item.position;
                
            }
            if (item.tag == "CupPosition")
            {
                _cupTransform.position = item.position;
            }
        }
        _showCurrentLevel.text = punkCrystallCurrentLevelIndex.ToString();
    }

    private void OnDestroy()
    {
        onShowResult.RemoveAllListeners();
    }

    private void OnResult(bool isLoose)
    {
        switch (isLoose)
        {
            case true:
                _resultTextDisplay.text = "YOU LOOOSE\nLEVEL NOT PASSED";
                _restartButton.SetActive(true);
                break;
            case false:
                _resultTextDisplay.text = "VICTORY\nLEVEL PASSED";
                _restartButton.SetActive(false);
                break;
        }
        if (punkCrystallCurrentLevelIndex == _levelPrefabs.Length)
        {
            _restartButton.SetActive(true);
        }
        _resultpage.SetActive(true);
    }

    public void OnClickNext()
    {
        PlayerPrefs.SetInt("PunkCrystallsSOgiidfugsduigdfsiogfds" + punkCrystallCurrentLevelIndex + "saves", 1);
        punkCrystallCurrentLevelIndex += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
