using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject howToPlay;

    [SerializeField]
    private TMP_Text showLastGameData;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("hwtOpenedArmy"))
        {
            howToPlay.SetActive(true);
            PlayerPrefs.SetInt("hwtOpenedArmy", 1);
        }
    }

    private void LateUpdate()
    {
        showLastGameData.text = GameManager.playerRecordCount.ToString() +" : "+ GameManager.enemieRecordCount.ToString();
    }

    public void Play(int SceneIndex) 
    {
        SceneManager.LoadScene(SceneIndex);
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
