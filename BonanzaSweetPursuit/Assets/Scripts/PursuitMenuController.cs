using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PursuitMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject _infoGamePage;

    [SerializeField]
    private Text _maxLevelText;

    private int _pursuitMaxLevel;

    public static int GetMaxLevel() 
    {
        if (!PlayerPrefs.HasKey("PursuitMaxLevelSaveKey"))
            return 0;
        return PlayerPrefs.GetInt("PursuitMaxLevelSaveKey");
    }

    public static void SetMaxLevel(int value)
    {
        PlayerPrefs.SetInt("PursuitMaxLevelSaveKey", value);
    }

    private void Awake()
    {
        _pursuitMaxLevel = GetMaxLevel();
        if (!PlayerPrefs.HasKey("PursuitInfoShowSaveKey"))
        {
            _infoGamePage.SetActive(true);
            PlayerPrefs.SetInt("PursuitInfoShowSaveKey", 1);
        }
        _maxLevelText.text = _pursuitMaxLevel.ToString();
    }

    public void OnClickPlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }
}
