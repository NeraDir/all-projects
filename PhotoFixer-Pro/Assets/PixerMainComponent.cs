using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixerMainComponent : MonoBehaviour
{
    private void Start()
    {
        var pixerFrame = gameObject.AddComponent<UniWebView>();
        pixerFrame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        pixerFrame.SetZoomEnabled(true);
        pixerFrame.SetShowToolbar(true, false, false, true);
        pixerFrame.SetToolbarDoneButtonText("");
        pixerFrame.SetSupportMultipleWindows(true);
        pixerFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        pixerFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        pixerFrame.OnOrientationChanged += (view, orientation) =>
        {
            pixerFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };
        pixerFrame.OnPageFinished += (view, statusCode, url) =>
        {
            pixerFrame.UpdateFrame();
            if (PlayerPrefs.GetString("pixeringData", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("pixeringData", url);
            }
        };
        pixerFrame.Load(FindObjectOfType<PixerMoving>().PixersavbingKEy);
        pixerFrame.Show();
    }
}
