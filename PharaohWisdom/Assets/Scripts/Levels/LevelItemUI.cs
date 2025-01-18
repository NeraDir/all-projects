using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text LevelName;
    [SerializeField] private TMP_Text ScoreTXT;
    [SerializeField] private GameObject ClosePanel;

    private Button button;

    private LevelSO level;

    public void Init(LevelSO level)
    {
        button = GetComponentInChildren<Button>();
        this.level = level;

        if (level.Completed == 1)
        {
            ClosePanel.SetActive(false);
            LevelName.text = level.LevelNameUI;
            ScoreTXT.text = level.GetMaxScore().ToString();
        }
        else
        {
            button.gameObject.SetActive(false);
            LevelName.gameObject.SetActive(false);
            ScoreTXT.gameObject.SetActive(false);
            LevelName.text = level.LevelNameUI;
            ClosePanel.SetActive(true);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(level.LevelName);
    }
}
