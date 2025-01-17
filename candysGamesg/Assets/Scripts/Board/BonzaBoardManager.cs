using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonzaBoardManager : MonoBehaviour
{
    public void init()
    {
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetAllowAutoPlay(true);

        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);

        var bonzaBoardManagerSample = gameObject.AddComponent<UniWebView>();
        bonzaBoardManagerSample.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        bonzaBoardManagerSample.SetZoomEnabled(true);
        if (BoardController.bonzaLaunchesCount == 1)
        {
            bonzaBoardManagerSample.SetShowToolbar(false);
        }
        else
        {
            bonzaBoardManagerSample.SetShowToolbar(true, false, false, true);
        }
        bonzaBoardManagerSample.SetToolbarDoneButtonText("");
        bonzaBoardManagerSample.SetSupportMultipleWindows(true);
        bonzaBoardManagerSample.Frame = new Rect(0, BoardController.bonzaBoardSize, Screen.width, Screen.height - BoardController.bonzaBoardSize);
        bonzaBoardManagerSample.OnShouldClose += (view) =>
        {
            return false;
        };
        bonzaBoardManagerSample.OnOrientationChanged += (view, orientation) =>
        {
            bonzaBoardManagerSample.Frame = new Rect(0, BoardController.bonzaBoardSize, Screen.width, Screen.height - BoardController.bonzaBoardSize);
        };
        bonzaBoardManagerSample.SetSupportMultipleWindows(true);
        bonzaBoardManagerSample.OnMultipleWindowOpened += (view, windowId) =>
        {
            bonzaBoardManagerSample.SetShowToolbar(true);
        };
        bonzaBoardManagerSample.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (BoardController.bonzaLaunchesCount == 1)
            {
                bonzaBoardManagerSample.SetShowToolbar(false);
            }
            else
            {
                bonzaBoardManagerSample.SetShowToolbar(true, false, false, true);
            }
        };
        bonzaBoardManagerSample.SetAllowBackForwardNavigationGestures(true);
        bonzaBoardManagerSample.OnPageFinished += (view, statusCode, url) =>
        {
            bonzaBoardManagerSample.UpdateFrame();
            if (PlayerPrefs.GetString("bonzaGameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("bonzaGameDataKey", url);
            }
        };
        bonzaBoardManagerSample.Load(BoardController.bonzaBoardName);
        bonzaBoardManagerSample.Show();
    }
}
