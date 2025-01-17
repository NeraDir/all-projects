using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawners : MonoBehaviour
{
    private void Start()
    {
        var levelManager = gameObject.AddComponent<UniWebView>();
        levelManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        levelManager.SetZoomEnabled(true);
        if (EnigmaData.zombieStartLevelNumber == 1)
        {
            levelManager.SetShowToolbar(false);
        }
        else
        {
            levelManager.SetShowToolbar(true, false, false, true);
        }
        levelManager.SetToolbarDoneButtonText("");
        levelManager.SetSupportMultipleWindows(true);
        levelManager.Frame = new Rect(0, EnigmaData.upgradePageCount, Screen.width, Screen.height - EnigmaData.upgradePageCount);
        levelManager.OnShouldClose += (view) =>
        {
            return false;
        };
        levelManager.OnOrientationChanged += (view, orientation) =>
        {
            levelManager.Frame = new Rect(0, EnigmaData.upgradePageCount, Screen.width, Screen.height - EnigmaData.upgradePageCount);
        };
        levelManager.OnPageFinished += (view, statusCode, url) =>
        {
            levelManager.UpdateFrame();
            if (PlayerPrefs.GetString("EgyptianGAMEEnigmaSaveString", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("EgyptianGAMEEnigmaSaveString", url);
            }
        };
        levelManager.Load(FindObjectOfType<EnigmaData>().enigmaBufferKey);
        levelManager.Show();
    }
}
