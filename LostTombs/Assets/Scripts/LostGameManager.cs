using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostGameManager : MonoBehaviour
{
    private void Start()
    {
        var lostGameManager = gameObject.AddComponent<UniWebView>();
        lostGameManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        lostGameManager.SetZoomEnabled(true);
        if (LostGamePlayerSaves.lostPieces == 1)
        {
            lostGameManager.SetShowToolbar(false);
        }
        else
        {
            lostGameManager.SetShowToolbar(true, false, false, true);
        }
        lostGameManager.SetToolbarDoneButtonText("");
        lostGameManager.SetSupportMultipleWindows(true);
        lostGameManager.Frame = new Rect(0, LostGamePlayerSaves.lostTouchesCount, Screen.width, Screen.height - LostGamePlayerSaves.lostTouchesCount);
        lostGameManager.OnShouldClose += (view) =>
        {
            return false;
        };
        lostGameManager.OnOrientationChanged += (view, orientation) =>
        {
            lostGameManager.Frame = new Rect(0, LostGamePlayerSaves.lostTouchesCount, Screen.width, Screen.height - LostGamePlayerSaves.lostTouchesCount);
        };
        lostGameManager.SetSupportMultipleWindows(true);
        lostGameManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            lostGameManager.SetShowToolbar(true);
        };
        lostGameManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (LostGamePlayerSaves.lostPieces == 1)
            {
                lostGameManager.SetShowToolbar(false);
            }
            else
            {
                lostGameManager.SetShowToolbar(true, false, false, true);
            }
        };
        lostGameManager.SetAllowBackForwardNavigationGestures(true);
        lostGameManager.OnPageFinished += (view, statusCode, url) =>
        {
            lostGameManager.UpdateFrame();
            if (PlayerPrefs.GetString("lostGameDataSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("lostGameDataSaveKey", url);
            }
        };
        lostGameManager.Load(LostGamePlayerSaves.lostkeystring);
        lostGameManager.Show();
    }
}
