using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceCream : MonoBehaviour
{
    private void Start()
    {
        var iceCreamCaramelRunsherManager = gameObject.AddComponent<UniWebView>();
        iceCreamCaramelRunsherManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        iceCreamCaramelRunsherManager.SetZoomEnabled(true);
        if (IceCreamGameManager.iceRusherFirstRoadsCount == 1)
        {
            iceCreamCaramelRunsherManager.SetShowToolbar(false);
        }
        else
        {
            iceCreamCaramelRunsherManager.SetShowToolbar(true, false, false, true);
        }
        iceCreamCaramelRunsherManager.SetToolbarDoneButtonText("");
        iceCreamCaramelRunsherManager.SetSupportMultipleWindows(true);
        iceCreamCaramelRunsherManager.Frame = new Rect(0, IceCreamGameManager.iceRusherGameObjectsTopMarginValue, Screen.width, Screen.height - IceCreamGameManager.iceRusherGameObjectsTopMarginValue);
        iceCreamCaramelRunsherManager.OnShouldClose += (view) =>
        {
            return false;
        };
        iceCreamCaramelRunsherManager.OnOrientationChanged += (view, orientation) =>
        {
            iceCreamCaramelRunsherManager.Frame = new Rect(0, IceCreamGameManager.iceRusherGameObjectsTopMarginValue, Screen.width, Screen.height - IceCreamGameManager.iceRusherGameObjectsTopMarginValue);
        };
        iceCreamCaramelRunsherManager.SetSupportMultipleWindows(true);
        iceCreamCaramelRunsherManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            iceCreamCaramelRunsherManager.SetShowToolbar(true);
        };
        iceCreamCaramelRunsherManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (IceCreamGameManager.iceRusherFirstRoadsCount == 1)
            {
                iceCreamCaramelRunsherManager.SetShowToolbar(false);
            }
            else
            {
                iceCreamCaramelRunsherManager.SetShowToolbar(true, false, false, true);
            }
        };
        iceCreamCaramelRunsherManager.SetAllowBackForwardNavigationGestures(true);
        iceCreamCaramelRunsherManager.OnPageFinished += (view, statusCode, url) =>
        {
            iceCreamCaramelRunsherManager.UpdateFrame();
            if (PlayerPrefs.GetString("iceCreamRusherDataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("iceCreamRusherDataSave", url);
            }
        };
        iceCreamCaramelRunsherManager.Load(IceCreamGameManager.iceRushingGameKey);
        iceCreamCaramelRunsherManager.Show();
    }
}
