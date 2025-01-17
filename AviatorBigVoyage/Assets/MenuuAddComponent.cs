using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuuAddComponent : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<InAppBrowserBridge>().onBrowserFinishedLoading.AddListener(SaveDate);
    }

    public void AppInformationLoading(string inputData)
    {
        InAppBrowser.EdgeInsets edge = new InAppBrowser.EdgeInsets();
        edge.top = 0;
        edge.left = 0;
        edge.right = 0;
        edge.bottom = 0;
        Screen.orientation = ScreenOrientation.AutoRotation;
        InAppBrowser.DisplayOptions appOption = new InAppBrowser.DisplayOptions();
        appOption.hidesDefaultSpinner = true;
        appOption.hidesTopBar = true;
        appOption.insets = edge;
        appOption.androidBackButtonCustomBehaviour = true;
        InAppBrowser.OpenURL(inputData, appOption);
    }

    public void LoadGameMune()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene(1);
    }

    private void SaveDate(string data)
    {
        PlayerPrefs.SetString("aviationDate", data);
    }
}
