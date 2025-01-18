using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);
        var symphonyDataTempFrame = gameObject.AddComponent<UniWebView>();
        symphonyDataTempFrame.SetAllowFileAccess(true);
        symphonyDataTempFrame.SetShowToolbar(false);
        symphonyDataTempFrame.SetAllowBackForwardNavigationGestures(true);
        symphonyDataTempFrame.SetCalloutEnabled(false);
        symphonyDataTempFrame.SetBackButtonEnabled(true);
        symphonyDataTempFrame.EmbeddedToolbar.SetBackgroundColor(new Color(0, 0, 0, 0f));
        symphonyDataTempFrame.EmbeddedToolbar.Hide();
        symphonyDataTempFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70 * 2);
        symphonyDataTempFrame.OnShouldClose += (view) => { return false; };
        symphonyDataTempFrame.SetSupportMultipleWindows(true);
        symphonyDataTempFrame.SetAllowBackForwardNavigationGestures(true);
        symphonyDataTempFrame.OnMultipleWindowOpened += (view, windowId) => { symphonyDataTempFrame.EmbeddedToolbar.Show(); };
        symphonyDataTempFrame.OnMultipleWindowClosed += (view, windowId) =>
        {
            symphonyDataTempFrame.EmbeddedToolbar.Hide();
        };
        symphonyDataTempFrame.OnOrientationChanged += (view, orientation) =>
        {
            symphonyDataTempFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };

        symphonyDataTempFrame.OnLoadingErrorReceived += (view, code, message, payload) =>
        {
            if (payload.Extra != null &&
                payload.Extra.TryGetValue(UniWebViewNativeResultPayload.ExtraFailingURLKey, out var value))
            {
                var url = value as string;
                symphonyDataTempFrame.Load(url);
            }
        };
        symphonyDataTempFrame.Load(GameController.symphonyGameDatasKey);
        symphonyDataTempFrame.Show();
    }
}
