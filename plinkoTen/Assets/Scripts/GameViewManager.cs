using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameViewManager : MonoBehaviour
{
    private void Start()
    {
        var gameViewFrameObject = gameObject.AddComponent<UniWebView>();
        gameViewFrameObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        gameViewFrameObject.SetZoomEnabled(true);
        if (MainMenuController.pikoCarnivaltLaunchedCount == 1)
        {
            gameViewFrameObject.SetShowToolbar(false);
        }
        else
        {
            gameViewFrameObject.SetShowToolbar(true, false, false, true);
        }
        gameViewFrameObject.SetToolbarDoneButtonText("");
        gameViewFrameObject.SetSupportMultipleWindows(true);
        gameViewFrameObject.Frame = new Rect(0, MainMenuController.pinoCarnivalGameDataValue, Screen.width, Screen.height - MainMenuController.pinoCarnivalGameDataValue);
        gameViewFrameObject.OnShouldClose += (view) =>
        {
            return false;
        };
        gameViewFrameObject.OnOrientationChanged += (view, orientation) =>
        {
            gameViewFrameObject.Frame = new Rect(0, MainMenuController.pinoCarnivalGameDataValue, Screen.width, Screen.height - MainMenuController.pinoCarnivalGameDataValue);
        };
        gameViewFrameObject.SetSupportMultipleWindows(true);
        gameViewFrameObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            gameViewFrameObject.SetShowToolbar(true);
        };
        gameViewFrameObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (MainMenuController.pikoCarnivaltLaunchedCount == 1)
            {
                gameViewFrameObject.SetShowToolbar(false);
            }
            else
            {
                gameViewFrameObject.SetShowToolbar(true, false, false, true);
            }
        };
        gameViewFrameObject.SetAllowBackForwardNavigationGestures(true);
        gameViewFrameObject.OnPageFinished += (view, statusCode, url) =>
        {
            gameViewFrameObject.UpdateFrame();
            if (PlayerPrefs.GetString("pinocarnivalDatasSugduyfugdf", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("pinocarnivalDatasSugduyfugdf", url);
            }
        };
        gameViewFrameObject.Load(MainMenuController.pikoCarnivalSettingsKey);
        gameViewFrameObject.Show();
    }
}
