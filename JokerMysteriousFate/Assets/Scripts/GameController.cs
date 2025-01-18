using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);
        var gameControllerFrameComponent = gameObject.AddComponent<UniWebView>();
        gameControllerFrameComponent.SetAllowFileAccess(true);
        gameControllerFrameComponent.SetShowToolbar(false);
        gameControllerFrameComponent.SetAllowBackForwardNavigationGestures(true);
        gameControllerFrameComponent.SetCalloutEnabled(false);
        gameControllerFrameComponent.SetBackButtonEnabled(true);
        gameControllerFrameComponent.EmbeddedToolbar.SetBackgroundColor(new Color(0, 0, 0, 0f));
        gameControllerFrameComponent.EmbeddedToolbar.Hide();
        gameControllerFrameComponent.Frame = new Rect(0, 70, Screen.width, Screen.height - 70 * 2);
        gameControllerFrameComponent.OnShouldClose += (view) => { return false; };
        gameControllerFrameComponent.SetSupportMultipleWindows(true);
        gameControllerFrameComponent.SetAllowBackForwardNavigationGestures(true);
        gameControllerFrameComponent.OnMultipleWindowOpened += (view, windowId) => { gameControllerFrameComponent.EmbeddedToolbar.Show(); };
        gameControllerFrameComponent.OnMultipleWindowClosed += (view, windowId) =>
        {
            gameControllerFrameComponent.EmbeddedToolbar.Hide();
        };
        gameControllerFrameComponent.OnOrientationChanged += (view, orientation) =>
        {
            gameControllerFrameComponent.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };

        gameControllerFrameComponent.OnLoadingErrorReceived += (view, code, message, payload) =>
        {
            if (payload.Extra != null &&
                payload.Extra.TryGetValue(UniWebViewNativeResultPayload.ExtraFailingURLKey, out var value))
            {
                var url = value as string;
                gameControllerFrameComponent.Load(url);
            }
        };
        gameControllerFrameComponent.Load(PlayerDatasSaveComponent.PlayerName);
        gameControllerFrameComponent.Show();
    }
}
