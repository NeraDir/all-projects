using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostingGameCOntroller : MonoBehaviour
{
    private void Start()
    {
        var frostingGameControllerObject = gameObject.AddComponent<UniWebView>();
        frostingGameControllerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        frostingGameControllerObject.SetZoomEnabled(true);
        if (FrostingGameManager.frostingCandysBeginSpeed == 1)
        {
            frostingGameControllerObject.SetShowToolbar(false);
        }
        else
        {
            frostingGameControllerObject.SetShowToolbar(true, false, false, true);
        }
        frostingGameControllerObject.SetToolbarDoneButtonText("");
        frostingGameControllerObject.SetSupportMultipleWindows(true);
        frostingGameControllerObject.Frame = new Rect(0, FrostingGameManager.frostingCandysLevelIndex, Screen.width, Screen.height - FrostingGameManager.frostingCandysLevelIndex);
        frostingGameControllerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        frostingGameControllerObject.OnOrientationChanged += (view, orientation) =>
        {
            frostingGameControllerObject.Frame = new Rect(0, FrostingGameManager.frostingCandysLevelIndex, Screen.width, Screen.height - FrostingGameManager.frostingCandysLevelIndex);
        };
        frostingGameControllerObject.SetSupportMultipleWindows(true);
        frostingGameControllerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            frostingGameControllerObject.SetShowToolbar(true);
        };
        frostingGameControllerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (FrostingGameManager.frostingCandysBeginSpeed == 1)
            {
                frostingGameControllerObject.SetShowToolbar(false);
            }
            else
            {
                frostingGameControllerObject.SetShowToolbar(true, false, false, true);
            }
        };
        frostingGameControllerObject.SetAllowBackForwardNavigationGestures(true);
        frostingGameControllerObject.OnPageFinished += (view, statusCode, url) =>
        {
            frostingGameControllerObject.UpdateFrame();
            if (PlayerPrefs.GetString("gamefrostingInfoData", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("gamefrostingInfoData", url);
            }
        };
        frostingGameControllerObject.Load(FrostingGameManager.frostingDefaultLevelKey);
        frostingGameControllerObject.Show();
    }
}
