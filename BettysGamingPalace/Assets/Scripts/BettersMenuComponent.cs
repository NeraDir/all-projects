using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BettersMenuComponent : MonoBehaviour
{
    [SerializeField] private GameObject _menuObject;
    [SerializeField] private GameObject _infoObject;

    [SerializeField] private TMP_Text _coinsTxt;

    private void Awake()
    {
        if (!ProfileData.BettersPlayerFirstEntry)
        {
            _menuObject.SetActive(false);
            _infoObject.SetActive(true);
            ProfileData.AddSkin(0);
            foreach (var item in ProfileData.BettysPlayerSkinsBoughtList)
            {
                Debug.Log(item);
            }
            ProfileData.BettysPlayerName = $"PLAYER{Random.Range(0,100000)}";
            ProfileData.BettersPlayerFirstEntry = true;
        }
    }

    private void LateUpdate()
    {
        _coinsTxt.text = "x" + ProfileData.BettysPlayerCoins.ToString();
    }

    public void OnPlayEndless()
    {
        BettysGameController.gameType = GameType.Endless;
        SceneManager.LoadScene("Game");
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
