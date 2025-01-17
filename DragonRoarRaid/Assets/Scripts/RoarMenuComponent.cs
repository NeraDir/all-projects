using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoarMenuComponent : MonoBehaviour
{
    [SerializeField]
    private Text maxLevelTxt;

    [SerializeField]
    private GameObject gameInfoPage;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("RoarGameInfoShowSaveKey"))
        {
            gameInfoPage.SetActive(true);
            PlayerPrefs.SetString("RoarGameInfoShowSaveKey", "showed");
        }
        maxLevelTxt.text = RoadGameComponent.RoarGameMaxReachedLevel.ToString("0");
    }

    public void OnClickExit() 
    {
        Application.Quit();
    }

    public void OnClickPlay() 
    {
        SceneManager.LoadScene("SampleScene");
    }
}
