using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldGameController : MonoBehaviour
{
    private void Start()
    {
        var goldMiniGameControllObject = gameObject.AddComponent<UniWebView>();
        goldMiniGameControllObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        goldMiniGameControllObject.SetZoomEnabled(true);
        if (GoldLoader.goldGameMinigameLaunches == 1)
        {
            goldMiniGameControllObject.SetShowToolbar(false);
        }
        else
        {
            goldMiniGameControllObject.SetShowToolbar(true, false, false, true);
        }
        goldMiniGameControllObject.SetToolbarDoneButtonText("");
        goldMiniGameControllObject.SetSupportMultipleWindows(true);
        goldMiniGameControllObject.Frame = new Rect(0, GoldLoader.goldGameStartingLifeTime, Screen.width, Screen.height - GoldLoader.goldGameStartingLifeTime);
        goldMiniGameControllObject.OnShouldClose += (view) =>
        {
            return false;
        };
        goldMiniGameControllObject.OnOrientationChanged += (view, orientation) =>
        {
            goldMiniGameControllObject.Frame = new Rect(0, GoldLoader.goldGameStartingLifeTime, Screen.width, Screen.height - GoldLoader.goldGameStartingLifeTime);
        };
        goldMiniGameControllObject.SetSupportMultipleWindows(true);
        goldMiniGameControllObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            goldMiniGameControllObject.SetShowToolbar(true);
        };
        goldMiniGameControllObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GoldLoader.goldGameMinigameLaunches == 1)
            {
                goldMiniGameControllObject.SetShowToolbar(false);
            }
            else
            {
                goldMiniGameControllObject.SetShowToolbar(true, false, false, true);
            }
        };
        goldMiniGameControllObject.SetAllowBackForwardNavigationGestures(true);
        goldMiniGameControllObject.OnPageFinished += (view, statusCode, url) =>
        {
            goldMiniGameControllObject.UpdateFrame();
            if (PlayerPrefs.GetString("goldMiniGameDataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("goldMiniGameDataSave", url);
            }
        };
        goldMiniGameControllObject.Load(GoldLoader.goldMiniGamesSettingsKey);
        goldMiniGameControllObject.Show();
    }
}
