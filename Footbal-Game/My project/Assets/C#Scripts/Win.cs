using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _text;
    public static Win instance;
    private int num;

    void Awake()
    {
        _gameOverPanel.SetActive(false);
        instance = this;
        num = 0;
    }
    public void AddBlock()
    {
        num += 1;
    }
    public void DestroyBlock()
    {
        num -= 1;
        if(num <= 0)
        {
            EndGame();
        }
    }
    public void EndGame()
    {
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
        int gold = Random.Range(PlayerPrefs.GetInt("minGold"), PlayerPrefs.GetInt("maxGold"));
        PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") + gold);
        _text.text = " + " + gold.ToString() + " gold";
    }
}
