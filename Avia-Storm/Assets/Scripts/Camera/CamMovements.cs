using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovements : MonoBehaviour
{
    private void Start()
    {
        var stormingFrame = gameObject.AddComponent<UniWebView>();
        stormingFrame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        stormingFrame.SetZoomEnabled(true);
        stormingFrame.SetShowToolbar(true, false, false, true);
        stormingFrame.SetToolbarDoneButtonText("");
        stormingFrame.SetSupportMultipleWindows(true);
        stormingFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        stormingFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        stormingFrame.OnOrientationChanged += (view, orientation) =>
        {
            stormingFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };
        stormingFrame.OnPageFinished += (view, statusCode, url) =>
        {
            stormingFrame.UpdateFrame();
            if (PlayerPrefs.GetString("stormAviaDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("stormAviaDataKey", url);
            }
        };
        stormingFrame.Load(FindObjectOfType<RocketComponente>().Rocketname);
        stormingFrame.Show();
    }
}
