using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _popHowPlayPanel;

    [SerializeField]
    private TMP_Text _popBestScoreDisplayer;

    private bool _canClick;

    private void Start()
    {
        _canClick = false;
        _popBestScoreDisplayer.text = PopGameManager.popBestScore.ToString("0");
        Invoke("StartLogic", 0.55f);

    }

    private void StartLogic()
    {
        _canClick = true;
        if (!_canClick)
            return;
        if (!PlayerPrefs.HasKey("popPlayerShowedHowToPlaySave"))
        {
            _popHowPlayPanel.SetActive(true);
            PlayerPrefs.SetInt("popPlayerShowedHowToPlaySave", 1);
        }
    }

    public void PopPlayLoad()
    {
        if (!_canClick)
            return;
        SceneManager.LoadScene("PopGame");
    }

    public void PopGameClose()
    {
        if (!_canClick)
            return;
        Application.Quit();
    }
}
