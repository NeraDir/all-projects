using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text LevelName;
    [SerializeField] private TMP_Text ScoreTXT;
    [SerializeField] private GameObject ClosePanel;

    private LevelSO level;

    public void Init(LevelSO level)
    {
        this.level = level;

        if (level.Completed == 1)
        {
            ClosePanel.SetActive(false);
            LevelName.text = level.LevelNameUI;
            ScoreTXT.text = level.GetMaxScore().ToString();
        }
        else
        {
            LevelName.text = level.LevelNameUI;
            ClosePanel.SetActive(true);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(level.LevelName);
    }
}
