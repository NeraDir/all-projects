using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainRoundManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _endText;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private GameObject[] _lvls;
    public static MainRoundManager instance;

    private int gold = 0;
    private void Awake()
    {
        Enemi.numEnemis = 0;
        _endPanel.SetActive(false);
        instance = this;
        foreach (var x in _lvls)
        {
            x.SetActive(false);
        }
        _lvls[PrefsControl.GetLvlNum()].SetActive(true);
    }
    public void AddGold(int g)
    {
        gold += g;
    }
    public void Win()
    {
        _endPanel.SetActive(true);
        _endText.text = "You win " + gold.ToString() + " gold";
        PrefsControl.FinisLvl(PrefsControl.GetLvlNum());
        PrefsControl.ChageGoald(gold);
    }
    public void Lose()
    {
        _endPanel.SetActive(true);
        _endText.text = "You lose";
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
