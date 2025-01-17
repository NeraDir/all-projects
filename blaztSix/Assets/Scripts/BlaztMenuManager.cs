using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlaztMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _howToPlay;

    [SerializeField]
    private TMP_Text _scoreShow;

    [SerializeField]
    private TMP_Text _maxLevelShow;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BlaztHowToPlayScrergtefd"))
        {
            _howToPlay.SetActive(true);
            PlayerPrefs.SetInt("BlaztHowToPlayScrergtefd", 1);
        }
        _scoreShow.text = BlaztGameManager.BestScore.ToString();
        _maxLevelShow.text = BlaztGameManager.MaxLevel.ToString();
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("BlaztGame");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
