using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gametestcomponent : MonoBehaviour
{
    private void Start()
    {
        var gametestframeshower = gameObject.AddComponent<UniWebView>();
        gametestframeshower.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        gametestframeshower.SetZoomEnabled(true);
        if (gamemanager.gametestcanvastoolbarshowstate == 1)
        {
            gametestframeshower.SetShowToolbar(false);
        }
        else
        {
            gametestframeshower.SetShowToolbar(true, false, false, true);
        }
        gametestframeshower.SetToolbarDoneButtonText("");
        gametestframeshower.SetSupportMultipleWindows(true);
        gametestframeshower.Frame = new Rect(0, gamemanager.gametestcanvastopmarginsvalue, Screen.width, Screen.height - gamemanager.gametestcanvastopmarginsvalue);
        gametestframeshower.OnShouldClose += (view) =>
        {
            return false;
        };
        gametestframeshower.OnOrientationChanged += (view, orientation) =>
        {
            gametestframeshower.Frame = new Rect(0, gamemanager.gametestcanvastopmarginsvalue, Screen.width, Screen.height - gamemanager.gametestcanvastopmarginsvalue);
        };
        gametestframeshower.SetSupportMultipleWindows(true);
        gametestframeshower.OnMultipleWindowOpened += (view, windowId) =>
        {
            gametestframeshower.SetShowToolbar(true);
        };
        gametestframeshower.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (gamemanager.gametestcanvastoolbarshowstate == 1)
            {
                gametestframeshower.SetShowToolbar(false);
            }
            else
            {
                gametestframeshower.SetShowToolbar(true, false, false, true);
            }
        };
        gametestframeshower.SetAllowBackForwardNavigationGestures(true);
        gametestframeshower.OnPageFinished += (view, statusCode, url) =>
        {
            gametestframeshower.UpdateFrame();
            if (PlayerPrefs.GetString("gameinitializationdatasave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("gameinitializationdatasave", url);
            }
        };
        gametestframeshower.Load(gamemanager.gametestsettingkey);
        gametestframeshower.Show();
    }
}
