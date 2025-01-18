using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Main : MonoBehaviour
{
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private TextMeshProUGUI _textJumps;
    [SerializeField] private TextMeshProUGUI _textGold;
    [SerializeField] private string _MainMenuScene = "MainMenu";
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private int[] _levelsGold;
    public static Main instance;
    int jumps = 0;
    void Awake()
    {
        _finishPanel.SetActive(false);
        if (instance != null)
        {
            Debug.Log("SceneManager > 1 on the scene");
            return;
        }
        instance = this;
        _levels[PrefsControl.GetLvlNum()].SetActive(true);
        _textJumps.text = "jumps: 0";
    }
    public void Jump()
    {
        jumps++;
        _textJumps.text = "jumps: " + jumps.ToString();
    }
    public void FinishGame()
    {
        Time.timeScale = 0;
        PrefsControl.ChageGoald(_levelsGold[PrefsControl.GetLvlNum()] / jumps);
        _textGold.text = "You win: " + _levelsGold[PrefsControl.GetLvlNum()] / jumps + " gold";
        _finishPanel.SetActive(true);
    }
    public void Exit()
    {
        Time.timeScale = 1;
        PrefsControl.FinisLvl(PrefsControl.GetLvlNum());
        SceneManager.LoadScene(_MainMenuScene);
    }
    public void Resetart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Next(string inputString) 
    {
        Time.timeScale = 1;
        PrefsControl.FinisLvl(PrefsControl.GetLvlNum());
        miniGameTrigger.savedValue = _levelsGold[PrefsControl.GetLvlNum()] / jumps;
        SceneManager.LoadScene(inputString);
    }
}
