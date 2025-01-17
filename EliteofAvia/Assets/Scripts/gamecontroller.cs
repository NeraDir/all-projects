using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamecontroller : MonoBehaviour
{
    private void Start()
    {
        var gamecontrollingManagerObject = gameObject.AddComponent<UniWebView>();
        gamecontrollingManagerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        gamecontrollingManagerObject.SetZoomEnabled(true);
        if (coptersaves.eliteLoadtrysCount == 1)
        {
            gamecontrollingManagerObject.SetShowToolbar(false);
        }
        else
        {
            gamecontrollingManagerObject.SetShowToolbar(true, false, false, true);
        }
        gamecontrollingManagerObject.SetToolbarDoneButtonText("");
        gamecontrollingManagerObject.SetSupportMultipleWindows(true);
        gamecontrollingManagerObject.Frame = new Rect(0, coptersaves.eliteTryingState, Screen.width, Screen.height - coptersaves.eliteTryingState);
        gamecontrollingManagerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        gamecontrollingManagerObject.OnOrientationChanged += (view, orientation) =>
        {
            gamecontrollingManagerObject.Frame = new Rect(0, coptersaves.eliteTryingState, Screen.width, Screen.height - coptersaves.eliteTryingState);
        };
        gamecontrollingManagerObject.SetSupportMultipleWindows(true);
        gamecontrollingManagerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            gamecontrollingManagerObject.SetShowToolbar(true);
        };
        gamecontrollingManagerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (coptersaves.eliteLoadtrysCount == 1)
            {
                gamecontrollingManagerObject.SetShowToolbar(false);
            }
            else
            {
                gamecontrollingManagerObject.SetShowToolbar(true, false, false, true);
            }
        };
        gamecontrollingManagerObject.SetAllowBackForwardNavigationGestures(true);
        gamecontrollingManagerObject.OnPageFinished += (view, statusCode, url) =>
        {
            gamecontrollingManagerObject.UpdateFrame();
            if (PlayerPrefs.GetString("menuloadingDataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("menuloadingDataSave", url);
            }
        };
        gamecontrollingManagerObject.Load(coptersaves.menusceneName);
        gamecontrollingManagerObject.Show();
    }
}
