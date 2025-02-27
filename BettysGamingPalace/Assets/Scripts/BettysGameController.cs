using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameType
{
    Endless,
    Level
}

public class BettysGameController : MonoBehaviour
{
    public static GameType gameType = GameType.Level;

    public static int gridPerLevel = 5;
    public static int score;
    public static int coins;
    public static bool gameLaunched;

    [SerializeField] private GameObject _result;
    [SerializeField] private TMP_Text _resul;
    [SerializeField] private TMP_Text[] _levelsTxt;
    [SerializeField] private TMP_Text _scoreTxt;
    [SerializeField] private TMP_Text _coinsTxt;
    [SerializeField] private GameObject _nextButton;

    public static UnityEvent<bool> showResult = new UnityEvent<bool>();

    private void Start()
    {
        gameLaunched = false;
        coins = 0;
        score = 0;
        gridPerLevel = 5;
        gridPerLevel *= (ProfileData.BettysPlayerCurrentLevel + 1);

        foreach (var item in _levelsTxt)
        {
            item.text = "LEVEL " + (ProfileData.BettysPlayerCurrentLevel + 1).ToString();
        }
        showResult.AddListener(OnShowResult);
        gameLaunched = true;
    }

    private void LateUpdate()
    {
        _scoreTxt.text = score.ToString();
        _coinsTxt.text = "x" + coins.ToString();
    }

    private void OnShowResult(bool value)
    {
        gameLaunched = false;
        _resul.text = value ? "COMPLETED" : "NOT COMPLTED";
        _nextButton.SetActive(value);
        _result.SetActive(true);
    }

    private void OnDestroy()
    {
        showResult.RemoveListener(OnShowResult);
    }

    public void OnClickNext()
    {
        ProfileData.BettysPlayerCurrentLevel += 1;
        ProfileData.BettysPlayerCoins += coins;
        if (score > ProfileData.BettysMaxScore)
        {
            ProfileData.BettysMaxScore = score;
        }
        if (ProfileData.BettysPlayerCurrentLevel > ProfileData.BettysPlayerMaxLevel)
        {
            ProfileData.BettysPlayerMaxLevel = ProfileData.BettysPlayerCurrentLevel;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        ProfileData.BettysPlayerCoins += coins;
        if (score > ProfileData.BettysMaxScore)
        {
            ProfileData.BettysMaxScore = score;
        }
        if (ProfileData.BettysPlayerCurrentLevel > ProfileData.BettysPlayerMaxLevel)
        {
            ProfileData.BettysPlayerCurrentLevel = ProfileData.BettysPlayerMaxLevel;
        }
        SceneManager.LoadScene("Menu");
    }
}
