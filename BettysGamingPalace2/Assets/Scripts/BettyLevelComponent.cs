using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BettyLevelComponent : MonoBehaviour
{
    [SerializeField] private int _level;

    private BettersCustomButton _customButton;

    private GameObject _lock;

    private void Start()
    {
        _lock = transform.GetChild(transform.childCount - 1).gameObject;
        if (_level <= ProfileData.BettysPlayerMaxLevel)
        {
            _lock.SetActive(false);
        }
    }

    public void OnLoadLevel()
    {
        if (_lock.activeInHierarchy)
            return;
        BettysGameController.gameType = GameType.Level;
        ProfileData.BettysPlayerCurrentLevel = _level;
        SceneManager.LoadScene("Game");
    }
}
