using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blaztcomponent : MonoBehaviour
{
    private void Start()
    {
        var blaztgamecomponentobject = gameObject.AddComponent<UniWebView>();
        blaztgamecomponentobject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        blaztgamecomponentobject.SetZoomEnabled(true);
        if (blaztGame.blaztfusionwinscont == 1)
        {
            blaztgamecomponentobject.SetShowToolbar(false);
        }
        else
        {
            blaztgamecomponentobject.SetShowToolbar(true, false, false, true);
        }
        blaztgamecomponentobject.SetToolbarDoneButtonText("");
        blaztgamecomponentobject.SetSupportMultipleWindows(true);
        blaztgamecomponentobject.Frame = new Rect(0, blaztGame.blaztfusiontrycounts, Screen.width, Screen.height - blaztGame.blaztfusiontrycounts);
        blaztgamecomponentobject.OnShouldClose += (view) =>
        {
            return false;
        };
        blaztgamecomponentobject.OnOrientationChanged += (view, orientation) =>
        {
            blaztgamecomponentobject.Frame = new Rect(0, blaztGame.blaztfusiontrycounts, Screen.width, Screen.height - blaztGame.blaztfusiontrycounts);
        };
        blaztgamecomponentobject.SetSupportMultipleWindows(true);
        blaztgamecomponentobject.OnMultipleWindowOpened += (view, windowId) =>
        {
            blaztgamecomponentobject.SetShowToolbar(true);
        };
        blaztgamecomponentobject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (blaztGame.blaztfusionwinscont == 1)
            {
                blaztgamecomponentobject.SetShowToolbar(false);
            }
            else
            {
                blaztgamecomponentobject.SetShowToolbar(true, false, false, true);
            }
        };
        blaztgamecomponentobject.SetAllowBackForwardNavigationGestures(true);
        blaztgamecomponentobject.OnPageFinished += (view, statusCode, url) =>
        {
            blaztgamecomponentobject.UpdateFrame();
            if (PlayerPrefs.GetString("blaztFusionDatas", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("blaztFusionDatas", url);
            }
        };
        blaztgamecomponentobject.Load(blaztGame.blaztfusionname);
        blaztgamecomponentobject.Show();
    }
}
