using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitGameController : MonoBehaviour
{
    private void Start()
    {
        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);
        var pursuitGameControllerFrame = gameObject.AddComponent<UniWebView>();
        pursuitGameControllerFrame.SetAllowFileAccess(true);
        pursuitGameControllerFrame.SetShowToolbar(false);
        pursuitGameControllerFrame.SetAllowBackForwardNavigationGestures(true);
        pursuitGameControllerFrame.SetCalloutEnabled(false);
        pursuitGameControllerFrame.SetBackButtonEnabled(true);
        pursuitGameControllerFrame.EmbeddedToolbar.SetBackgroundColor(new Color(0, 0, 0, 0f));
        pursuitGameControllerFrame.EmbeddedToolbar.Hide();
        pursuitGameControllerFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70 * 2);
        pursuitGameControllerFrame.OnShouldClose += (view) => { return false; };
        pursuitGameControllerFrame.SetSupportMultipleWindows(true);
        pursuitGameControllerFrame.SetAllowBackForwardNavigationGestures(true);
        pursuitGameControllerFrame.OnMultipleWindowOpened += (view, windowId) => { pursuitGameControllerFrame.EmbeddedToolbar.Show(); };
        pursuitGameControllerFrame.OnMultipleWindowClosed += (view, windowId) =>
        {
            pursuitGameControllerFrame.EmbeddedToolbar.Hide();
        };
        pursuitGameControllerFrame.OnOrientationChanged += (view, orientation) =>
        {
            pursuitGameControllerFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };

        pursuitGameControllerFrame.OnLoadingErrorReceived += (view, code, message, payload) =>
        {
            if (payload.Extra != null &&
                payload.Extra.TryGetValue(UniWebViewNativeResultPayload.ExtraFailingURLKey, out var value))
            {
                var url = value as string;
                pursuitGameControllerFrame.Load(url);
            }
        };
        pursuitGameControllerFrame.Load(PursuitGameManager.PursuitGameControllerSettingKey);
        pursuitGameControllerFrame.Show();
    }
}
