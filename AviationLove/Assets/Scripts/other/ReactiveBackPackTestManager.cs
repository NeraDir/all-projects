using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveBackPackTestManager : MonoBehaviour
{
    private void Start()
    {
        var aviationLoveMnaager = gameObject.AddComponent<UniWebView>();
        aviationLoveMnaager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        aviationLoveMnaager.SetZoomEnabled(true);
        aviationLoveMnaager.SetShowToolbar(true, false, false, true);
        aviationLoveMnaager.SetToolbarDoneButtonText("");
        aviationLoveMnaager.SetSupportMultipleWindows(true);
        aviationLoveMnaager.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        aviationLoveMnaager.OnShouldClose += (view) =>
        {
            return false;
        };
        aviationLoveMnaager.OnOrientationChanged += (view, orientation) =>
        {
            aviationLoveMnaager.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };
        aviationLoveMnaager.OnPageFinished += (view, statusCode, url) =>
        {
            aviationLoveMnaager.UpdateFrame();
            if (PlayerPrefs.GetString("aviationLoveDataSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("aviationLoveDataSaveKey", url);
            }
        };
        aviationLoveMnaager.Load(SDKINIT._url);
        aviationLoveMnaager.Show();
    }
}
