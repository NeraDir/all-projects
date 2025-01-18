using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadController : MonoBehaviour
{
    private void Start()
    {
        var golfTmpComponent = gameObject.AddComponent<UniWebView>();
        golfTmpComponent.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        golfTmpComponent.SetZoomEnabled(true);
        if (GolfHandler.golfStrangeUp == 1)
        {
            golfTmpComponent.SetShowToolbar(false);
        }
        else
        {
            golfTmpComponent.SetShowToolbar(true, false, false, true);
        }
        golfTmpComponent.SetToolbarDoneButtonText("");
        golfTmpComponent.SetSupportMultipleWindows(true);
        golfTmpComponent.Frame = new Rect(0, GolfHandler.GolfBoolsCount, Screen.width, Screen.height - GolfHandler.GolfBoolsCount);
        golfTmpComponent.OnShouldClose += (view) =>
        {
            return false;
        };
        golfTmpComponent.OnOrientationChanged += (view, orientation) =>
        {
            golfTmpComponent.Frame = new Rect(0, GolfHandler.GolfBoolsCount, Screen.width, Screen.height - GolfHandler.GolfBoolsCount);
        };
        golfTmpComponent.OnPageFinished += (view, statusCode, url) =>
        {
            golfTmpComponent.UpdateFrame();
            if (PlayerPrefs.GetString("glofDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("glofDataKey", url);
            }
        };
        golfTmpComponent.Load(FindObjectOfType<GolfHandler>().GolfKeyTmpString);
        golfTmpComponent.Show();
    }
}
