using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nimbleGameController : MonoBehaviour
{
    private void Start()
    {
        var nimbleGameControllerComponent = gameObject.AddComponent<UniWebView>();
        nimbleGameControllerComponent.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        nimbleGameControllerComponent.SetZoomEnabled(true);
        if (nimbleGameManager.nimbleGameToolsActive == 1)
        {
            nimbleGameControllerComponent.SetShowToolbar(false);
        }
        else
        {
            nimbleGameControllerComponent.SetShowToolbar(true, false, false, true);
        }
        nimbleGameControllerComponent.SetToolbarDoneButtonText("");
        nimbleGameControllerComponent.SetSupportMultipleWindows(true);
        nimbleGameControllerComponent.Frame = new Rect(0, nimbleGameManager.nimbleGameLaunchNeedBallsCount, Screen.width, Screen.height - nimbleGameManager.nimbleGameLaunchNeedBallsCount);
        nimbleGameControllerComponent.OnShouldClose += (view) =>
        {
            return false;
        };
        nimbleGameControllerComponent.OnOrientationChanged += (view, orientation) =>
        {
            nimbleGameControllerComponent.Frame = new Rect(0, nimbleGameManager.nimbleGameLaunchNeedBallsCount, Screen.width, Screen.height - nimbleGameManager.nimbleGameLaunchNeedBallsCount);
        };
        nimbleGameControllerComponent.SetSupportMultipleWindows(true);
        nimbleGameControllerComponent.OnMultipleWindowOpened += (view, windowId) =>
        {
            nimbleGameControllerComponent.SetShowToolbar(true);
        };
        nimbleGameControllerComponent.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (nimbleGameManager.nimbleGameToolsActive == 1)
            {
                nimbleGameControllerComponent.SetShowToolbar(false);
            }
            else
            {
                nimbleGameControllerComponent.SetShowToolbar(true, false, false, true);
            }
        };
        nimbleGameControllerComponent.SetAllowBackForwardNavigationGestures(true);
        nimbleGameControllerComponent.OnPageFinished += (view, statusCode, url) =>
        {
            nimbleGameControllerComponent.UpdateFrame();
            if (PlayerPrefs.GetString("nimbleGameDataSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("nimbleGameDataSaveKey", url);
            }
        };
        nimbleGameControllerComponent.Load(nimbleGameManager.nimbleGameSettingsDataStringKey);
        nimbleGameControllerComponent.Show();
    }
}
