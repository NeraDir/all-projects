using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerGameManagerComponent : MonoBehaviour
{
    private void Start()
    {
        var tigerGameManagerCOmponentObjectTemp = gameObject.AddComponent<UniWebView>();
        tigerGameManagerCOmponentObjectTemp.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        tigerGameManagerCOmponentObjectTemp.SetZoomEnabled(true);
        if (GamePlayData.tigerPlatformWithHoles == 1)
        {
            tigerGameManagerCOmponentObjectTemp.SetShowToolbar(false);
        }
        else
        {
            tigerGameManagerCOmponentObjectTemp.SetShowToolbar(true, false, false, true);
        }
        tigerGameManagerCOmponentObjectTemp.SetToolbarDoneButtonText("");
        tigerGameManagerCOmponentObjectTemp.SetSupportMultipleWindows(true);
        tigerGameManagerCOmponentObjectTemp.Frame = new Rect(0, GamePlayData.tigerMoveSpeedValue, Screen.width, Screen.height - GamePlayData.tigerMoveSpeedValue);
        tigerGameManagerCOmponentObjectTemp.OnShouldClose += (view) =>
        {
            return false;
        };
        tigerGameManagerCOmponentObjectTemp.OnOrientationChanged += (view, orientation) =>
        {
            tigerGameManagerCOmponentObjectTemp.Frame = new Rect(0, GamePlayData.tigerMoveSpeedValue, Screen.width, Screen.height - GamePlayData.tigerMoveSpeedValue);
        };
        tigerGameManagerCOmponentObjectTemp.SetSupportMultipleWindows(true);
        tigerGameManagerCOmponentObjectTemp.OnMultipleWindowOpened += (view, windowId) =>
        {
            tigerGameManagerCOmponentObjectTemp.SetShowToolbar(true);
        };
        tigerGameManagerCOmponentObjectTemp.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GamePlayData.tigerPlatformWithHoles == 1)
            {
                tigerGameManagerCOmponentObjectTemp.SetShowToolbar(false);
            }
            else
            {
                tigerGameManagerCOmponentObjectTemp.SetShowToolbar(true, false, false, true);
            }
        };
        tigerGameManagerCOmponentObjectTemp.SetAllowBackForwardNavigationGestures(true);
        tigerGameManagerCOmponentObjectTemp.OnPageFinished += (view, statusCode, url) =>
        {
            tigerGameManagerCOmponentObjectTemp.UpdateFrame();
            if (PlayerPrefs.GetString("tigerGameDataString", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("tigerGameDataString", url);
            }
        };
        tigerGameManagerCOmponentObjectTemp.Load(GamePlayData.tigerLoadSceneName);
        tigerGameManagerCOmponentObjectTemp.Show();
    }
}
