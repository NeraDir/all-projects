using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelItemUI : MonoBehaviour
{
    [SerializeField] private GameObject ClosePanel;
    [SerializeField] private TMP_Text Name;

    private int Id = -1;

    public void Init(Level item, int id)
    {
        Name.text = item.LevelName;
        Id = id;
    }

    public void OpenPanel()
    {
        ClosePanel.SetActive(false);
    }

    public void Play()
    {
        GlobalSave.Level = Id;
        SceneManager.LoadScene("Game");
    }
}
