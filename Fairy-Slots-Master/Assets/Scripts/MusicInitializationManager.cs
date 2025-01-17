using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInitializationManager : MonoBehaviour
{
    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("fairySaveDataKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<SettingsInitializationManager>().FairyGameLaunch(PlayerPrefs.GetString("fairySaveDataKey"));
            }
            else
            {
                string tempFairyString = "";
                foreach (var fairyKey in FindObjectOfType<GameInitializationmanager>().fairyPiecesList)
                {
                    tempFairyString += fairyKey;
                }
                StartCoroutine(FindObjectOfType<SettingsInitializationManager>().LoadingFairy(tempFairyString));
            }
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
            StartCoroutine(FindObjectOfType<SceneManagement>().setScenesState());
        }
    }
}
