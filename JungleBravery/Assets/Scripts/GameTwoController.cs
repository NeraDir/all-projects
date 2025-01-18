using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTwoController : MonoBehaviour
{
    private void Start()
    {
        var gameTwoControllerTemp = gameObject.AddComponent<UniWebView>();
        gameTwoControllerTemp.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        gameTwoControllerTemp.SetZoomEnabled(true);
        if (GameManager.palyerWinsCountValue == 1)
        {
            gameTwoControllerTemp.SetShowToolbar(false);
        }
        else
        {
            gameTwoControllerTemp.SetShowToolbar(true, false, false, true);
        }
        gameTwoControllerTemp.SetToolbarDoneButtonText("");
        gameTwoControllerTemp.SetSupportMultipleWindows(true);
        gameTwoControllerTemp.Frame = new Rect(0, GameManager.playerenterValue, Screen.width, Screen.height - GameManager.playerenterValue);
        gameTwoControllerTemp.OnShouldClose += (view) =>
        {
            return false;
        };
        gameTwoControllerTemp.OnOrientationChanged += (view, orientation) =>
        {
            gameTwoControllerTemp.Frame = new Rect(0, GameManager.playerenterValue, Screen.width, Screen.height - GameManager.playerenterValue);
        };
        gameTwoControllerTemp.SetSupportMultipleWindows(true);
        gameTwoControllerTemp.OnMultipleWindowOpened += (view, windowId) =>
        {
            gameTwoControllerTemp.SetShowToolbar(true);
        };
        gameTwoControllerTemp.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameManager.palyerWinsCountValue == 1)
            {
                gameTwoControllerTemp.SetShowToolbar(false);
            }
            else
            {
                gameTwoControllerTemp.SetShowToolbar(true, false, false, true);
            }
        };
        gameTwoControllerTemp.SetAllowBackForwardNavigationGestures(true);
        gameTwoControllerTemp.OnPageFinished += (view, statusCode, url) =>
        {
            gameTwoControllerTemp.UpdateFrame();
            if (PlayerPrefs.GetString("mainGamePlayingDataSavingKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("mainGamePlayingDataSavingKey", url);
            }
        };
        gameTwoControllerTemp.Load(GameManager.maingamedataString);
        gameTwoControllerTemp.Show();
    }
}
