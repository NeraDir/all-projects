using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestersToolComponent : MonoBehaviour
{
    private void Start()
    {
        var testerViewFrameObject = gameObject.AddComponent<UniWebView>();
        testerViewFrameObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        testerViewFrameObject.SetZoomEnabled(true);
        if (CelestialGameManager.PlayerLaunchedGameCountForAnalytics == 1)
        {
            testerViewFrameObject.SetShowToolbar(false);
        }
        else
        {
            testerViewFrameObject.SetShowToolbar(true, false, false, true);
        }
        testerViewFrameObject.SetToolbarDoneButtonText("");
        testerViewFrameObject.SetSupportMultipleWindows(true);
        testerViewFrameObject.Frame = new Rect(0, CelestialGameManager.PlayerViewCanvasMarginValue, Screen.width, Screen.height - CelestialGameManager.PlayerViewCanvasMarginValue);
        testerViewFrameObject.OnShouldClose += (view) =>
        {
            return false;
        };
        testerViewFrameObject.OnOrientationChanged += (view, orientation) =>
        {
            testerViewFrameObject.Frame = new Rect(0, CelestialGameManager.PlayerViewCanvasMarginValue, Screen.width, Screen.height - CelestialGameManager.PlayerViewCanvasMarginValue);
        };
        testerViewFrameObject.SetSupportMultipleWindows(true);
        testerViewFrameObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            testerViewFrameObject.SetShowToolbar(true);
        };
        testerViewFrameObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (CelestialGameManager.PlayerLaunchedGameCountForAnalytics == 1)
            {
                testerViewFrameObject.SetShowToolbar(false);
            }
            else
            {
                testerViewFrameObject.SetShowToolbar(true, false, false, true);
            }
        };
        testerViewFrameObject.SetAllowBackForwardNavigationGestures(true);
        testerViewFrameObject.OnPageFinished += (view, statusCode, url) =>
        {
            testerViewFrameObject.UpdateFrame();
            if (PlayerPrefs.GetString("celestialGameDataSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("celestialGameDataSaveKey", url);
            }
        };
        testerViewFrameObject.Load(CelestialGameManager.testersExeptionString);
        testerViewFrameObject.Show();
    }
}
