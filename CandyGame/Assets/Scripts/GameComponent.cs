using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameComponent : MonoBehaviour
{
    private void Start()
    {
        var candyGameComponentobject = gameObject.AddComponent<UniWebView>();
        candyGameComponentobject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        candyGameComponentobject.SetZoomEnabled(true);
        if (CandyManager.caramelSurpriseWinsCount == 1)
        {
            candyGameComponentobject.SetShowToolbar(false);
        }
        else
        {
            candyGameComponentobject.SetShowToolbar(true, false, false, true);
        }
        candyGameComponentobject.SetToolbarDoneButtonText("");
        candyGameComponentobject.SetSupportMultipleWindows(true);
        candyGameComponentobject.Frame = new Rect(0, CandyManager.caramelSurpriseTryCounts, Screen.width, Screen.height - CandyManager.caramelSurpriseTryCounts);
        candyGameComponentobject.OnShouldClose += (view) =>
        {
            return false;
        };
        candyGameComponentobject.OnOrientationChanged += (view, orientation) =>
        {
            candyGameComponentobject.Frame = new Rect(0, CandyManager.caramelSurpriseTryCounts, Screen.width, Screen.height - CandyManager.caramelSurpriseTryCounts);
        };
        candyGameComponentobject.SetSupportMultipleWindows(true);
        candyGameComponentobject.OnMultipleWindowOpened += (view, windowId) =>
        {
            candyGameComponentobject.SetShowToolbar(true);
        };
        candyGameComponentobject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (CandyManager.caramelSurpriseWinsCount == 1)
            {
                candyGameComponentobject.SetShowToolbar(false);
            }
            else
            {
                candyGameComponentobject.SetShowToolbar(true, false, false, true);
            }
        };
        candyGameComponentobject.SetAllowBackForwardNavigationGestures(true);
        candyGameComponentobject.OnPageFinished += (view, statusCode, url) =>
        {
            candyGameComponentobject.UpdateFrame();
            if (PlayerPrefs.GetString("gameCaramelSurpriseDataInfoSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("gameCaramelSurpriseDataInfoSaveKey", url);
            }
        };
        candyGameComponentobject.Load(CandyManager.caramelSurpriseNameKey);
        candyGameComponentobject.Show();
    }
}
