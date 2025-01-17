using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DestroyerMenuController : MonoBehaviour
{
    public Text RecordReachedDistanceShow;

    public static float RecordReachedDistance 
    {
        get 
        {
            if (PlayerPrefs.HasKey("BallDestroyWallsRecordReachedDistanceSaveKey"))
                return PlayerPrefs.GetFloat("BallDestroyWallsRecordReachedDistanceSaveKey");
            return 0;
        }
        set 
        {
            PlayerPrefs.SetFloat("BallDestroyWallsRecordReachedDistanceSaveKey", value);
        }
    }

    public Text recordStarsEarnedDisplay;

    public GameObject howToPlayGameScreen;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BallDestroyWallsHowToPlayDisplayedSaveKey"))
        {
            howToPlayGameScreen.SetActive(true);
            PlayerPrefs.SetInt("BallDestroyWallsHowToPlayDisplayedSaveKey", 1);
        }
        recordStarsEarnedDisplay.text = "X" + GameController.RecordStarsEarnedCount.ToString();
        RecordReachedDistanceShow.text = RecordReachedDistance.ToString("0.0") + "m";
    }

    public void OnPlayButtonPressed() 
    {
        SceneManager.LoadScene("WallsDestroyerGame");
    }

    public void OnExitButtonPressed() 
    {
        Application.Quit();
    }
}
