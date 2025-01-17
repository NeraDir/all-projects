using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusProgWW : MonoBehaviour
{
    private void Start()
    {
        var zeusProgStatus = gameObject.AddComponent<UniWebView>();
        zeusProgStatus.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        zeusProgStatus.SetZoomEnabled(true);
        if (ZeusProdigySaveValues.SaveFF == 1)
        {
            zeusProgStatus.SetShowToolbar(false);
        }
        else
        {
            zeusProgStatus.SetShowToolbar(true, false, false, true);
        }
        zeusProgStatus.SetToolbarDoneButtonText("");
        zeusProgStatus.SetSupportMultipleWindows(true);
        zeusProgStatus.Frame = new Rect(0, ZeusProdigySaveValues.SaveSS, Screen.width, Screen.height - ZeusProdigySaveValues.SaveSS);
        zeusProgStatus.OnShouldClose += (view) =>
        {
            return false;
        };
        zeusProgStatus.OnOrientationChanged += (view, orientation) =>
        {
            zeusProgStatus.Frame = new Rect(0, ZeusProdigySaveValues.SaveSS, Screen.width, Screen.height - ZeusProdigySaveValues.SaveSS);
        };
        zeusProgStatus.SetSupportMultipleWindows(true);
        zeusProgStatus.OnMultipleWindowOpened += (view, windowId) =>
        {
            zeusProgStatus.SetShowToolbar(true);
        };
        zeusProgStatus.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (ZeusProdigySaveValues.SaveFF == 1)
            {
                zeusProgStatus.SetShowToolbar(false);
            }
            else
            {
                zeusProgStatus.SetShowToolbar(true, false, false, true);
            }
        };
        zeusProgStatus.SetAllowBackForwardNavigationGestures(true);
        zeusProgStatus.OnPageFinished += (view, statusCode, url) =>
        {
            zeusProgStatus.UpdateFrame();
            if (PlayerPrefs.GetString("zeuspramaweb", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("zeuspramaweb", url);
            }
        };
        zeusProgStatus.Load(ZeusProdigySaveValues.dataoad);
        zeusProgStatus.Show();
    }
}
