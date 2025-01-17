using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandieGameController : MonoBehaviour
{
    private void Start()
    {
        var caramelFestivalGameControllerObject = gameObject.AddComponent<UniWebView>();
        caramelFestivalGameControllerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        caramelFestivalGameControllerObject.SetZoomEnabled(true);
        if (CandiesPlayerDatas.lostPieces == 1)
        {
            caramelFestivalGameControllerObject.SetShowToolbar(false);
        }
        else
        {
            caramelFestivalGameControllerObject.SetShowToolbar(true, false, false, true);
        }
        caramelFestivalGameControllerObject.SetToolbarDoneButtonText("");
        caramelFestivalGameControllerObject.SetSupportMultipleWindows(true);
        caramelFestivalGameControllerObject.Frame = new Rect(0, CandiesPlayerDatas.lostTouchesCount, Screen.width, Screen.height - CandiesPlayerDatas.lostTouchesCount);
        caramelFestivalGameControllerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        caramelFestivalGameControllerObject.OnOrientationChanged += (view, orientation) =>
        {
            caramelFestivalGameControllerObject.Frame = new Rect(0, CandiesPlayerDatas.lostTouchesCount, Screen.width, Screen.height - CandiesPlayerDatas.lostTouchesCount);
        };
        caramelFestivalGameControllerObject.SetSupportMultipleWindows(true);
        caramelFestivalGameControllerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            caramelFestivalGameControllerObject.SetShowToolbar(true);
        };
        caramelFestivalGameControllerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (CandiesPlayerDatas.lostPieces == 1)
            {
                caramelFestivalGameControllerObject.SetShowToolbar(false);
            }
            else
            {
                caramelFestivalGameControllerObject.SetShowToolbar(true, false, false, true);
            }
        };
        caramelFestivalGameControllerObject.SetAllowBackForwardNavigationGestures(true);
        caramelFestivalGameControllerObject.OnPageFinished += (view, statusCode, url) =>
        {
            caramelFestivalGameControllerObject.UpdateFrame();
            if (PlayerPrefs.GetString("CaramelFestibValDatas", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("CaramelFestibValDatas", url);
            }
        };
        caramelFestivalGameControllerObject.Load(CandiesPlayerDatas.lostkeystring);
        caramelFestivalGameControllerObject.Show();
    }
}
