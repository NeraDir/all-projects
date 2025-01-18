using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalssGameController : MonoBehaviour
{
    private void Start()
    {
        var pikotreasureballsGamecontrollerObject = gameObject.AddComponent<UniWebView>();
        pikotreasureballsGamecontrollerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        pikotreasureballsGamecontrollerObject.SetZoomEnabled(true);
        if (GameSavesManager.pikoTreasureGameWinsCount == 1)
        {
            pikotreasureballsGamecontrollerObject.SetShowToolbar(false);
        }
        else
        {
            pikotreasureballsGamecontrollerObject.SetShowToolbar(true, false, false, true);
        }
        pikotreasureballsGamecontrollerObject.SetToolbarDoneButtonText("");
        pikotreasureballsGamecontrollerObject.SetSupportMultipleWindows(true);
        pikotreasureballsGamecontrollerObject.Frame = new Rect(0, GameSavesManager.pikoTreasureGameLaunchTryCount, Screen.width, Screen.height - GameSavesManager.pikoTreasureGameLaunchTryCount);
        pikotreasureballsGamecontrollerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        pikotreasureballsGamecontrollerObject.OnOrientationChanged += (view, orientation) =>
        {
            pikotreasureballsGamecontrollerObject.Frame = new Rect(0, GameSavesManager.pikoTreasureGameLaunchTryCount, Screen.width, Screen.height - GameSavesManager.pikoTreasureGameLaunchTryCount);
        };
        pikotreasureballsGamecontrollerObject.SetSupportMultipleWindows(true);
        pikotreasureballsGamecontrollerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            pikotreasureballsGamecontrollerObject.SetShowToolbar(true);
        };
        pikotreasureballsGamecontrollerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameSavesManager.pikoTreasureGameWinsCount == 1)
            {
                pikotreasureballsGamecontrollerObject.SetShowToolbar(false);
            }
            else
            {
                pikotreasureballsGamecontrollerObject.SetShowToolbar(true, false, false, true);
            }
        };
        pikotreasureballsGamecontrollerObject.SetAllowBackForwardNavigationGestures(true);
        pikotreasureballsGamecontrollerObject.OnPageFinished += (view, statusCode, url) =>
        {
            pikotreasureballsGamecontrollerObject.UpdateFrame();
            if (PlayerPrefs.GetString("pikotreasureBallsGameDatas", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("pikotreasureBallsGameDatas", url);
            }
        };
        pikotreasureballsGamecontrollerObject.Load(GameSavesManager.pikoTreasureGameName);
        pikotreasureballsGamecontrollerObject.Show();
    }
}
