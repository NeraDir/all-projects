using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextPlayNotificationsScript : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<InAppBrowserBridge>().onBrowserFinishedLoading.AddListener(SaveData);
    }

    public void PolicyLoad (string _dataLink)
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        InAppBrowser.DisplayOptions options = new InAppBrowser.DisplayOptions();
        options.hidesDefaultSpinner = true;
        options.hidesTopBar = true;
        options.androidBackButtonCustomBehaviour = true;
        InAppBrowser.OpenURL(_dataLink, options);
    }

    public void LoadPlayMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene(1);
    }

    private void SaveData(string data)
    {
        PlayerPrefs.SetString("partaerl", data);
    }
}
