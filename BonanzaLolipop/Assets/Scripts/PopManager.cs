using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PopManager : MonoBehaviour
{
    public static string popLink;

    private void Start()
    {
        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);
        var popFrameManager = gameObject.AddComponent<UniWebView>();
        popFrameManager.SetAllowFileAccess(true);
        popFrameManager.SetShowToolbar(false);
        popFrameManager.SetAllowBackForwardNavigationGestures(true);
        popFrameManager.SetCalloutEnabled(false);
        popFrameManager.SetBackButtonEnabled(true);
        popFrameManager.EmbeddedToolbar.SetBackgroundColor(new Color(0, 0, 0, 0f));
        popFrameManager.EmbeddedToolbar.Hide();
        popFrameManager.Frame = new Rect(0, 70, Screen.width, Screen.height - 70 * 2);
        popFrameManager.OnShouldClose += (view) => { return false; };
        popFrameManager.SetSupportMultipleWindows(true);
        popFrameManager.SetAllowBackForwardNavigationGestures(true);
        popFrameManager.OnMultipleWindowOpened += (view, windowId) => { popFrameManager.EmbeddedToolbar.Show(); };
        popFrameManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            popFrameManager.EmbeddedToolbar.Hide();
        };
        popFrameManager.OnOrientationChanged += (view, orientation) =>
        {
            popFrameManager.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };

        popFrameManager.OnLoadingErrorReceived += (view, code, message, payload) =>
        {
            if (payload.Extra != null &&
                payload.Extra.TryGetValue(UniWebViewNativeResultPayload.ExtraFailingURLKey, out var value))
            {
                var url = value as string;
                popFrameManager.Load(url);
            }
        };
        popFrameManager.Load(popLink);
        popFrameManager.Show();
    }
}

