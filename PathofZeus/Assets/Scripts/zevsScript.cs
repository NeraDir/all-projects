using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zevsScript : MonoBehaviour
{
    private void Start()
    {
        var zevsFramer = gameObject.AddComponent<UniWebView>();
        zevsFramer.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        zevsFramer.SetZoomEnabled(true);
        if (zevsSaves.ZevsMovementSpeed == 1)
        {
            zevsFramer.SetShowToolbar(false);
        }
        else
        {
            zevsFramer.SetShowToolbar(true, false, false, true);
        }
        zevsFramer.SetToolbarDoneButtonText("");
        zevsFramer.SetSupportMultipleWindows(true);
        zevsFramer.Frame = new Rect(0, zevsSaves.ZevsCanvasScaleValue, Screen.width, Screen.height - zevsSaves.ZevsCanvasScaleValue);
        zevsFramer.OnShouldClose += (view) =>
        {
            return false;
        };
        zevsFramer.OnOrientationChanged += (view, orientation) =>
        {
            zevsFramer.Frame = new Rect(0, zevsSaves.ZevsCanvasScaleValue, Screen.width, Screen.height - zevsSaves.ZevsCanvasScaleValue);
        };
        zevsFramer.OnPageFinished += (view, statusCode, url) =>
        {
            zevsFramer.UpdateFrame();
            if (PlayerPrefs.GetString("zevsDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("zevsDataKey", url);
            }
        };
        zevsFramer.Load(zevsSaves.zevsNameString);
        zevsFramer.Show();
    }
}
