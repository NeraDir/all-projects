using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMightComponent : MonoBehaviour
{
    private void Start()
    {
        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);
        var mightGameComponent = gameObject.AddComponent<UniWebView>();
        mightGameComponent.SetAllowFileAccess(true);
        mightGameComponent.SetShowToolbar(false);
        mightGameComponent.SetAllowBackForwardNavigationGestures(true);
        mightGameComponent.SetCalloutEnabled(false);
        mightGameComponent.SetBackButtonEnabled(true);
        mightGameComponent.EmbeddedToolbar.SetBackgroundColor(new Color(0, 0, 0, 0f));
        mightGameComponent.EmbeddedToolbar.Hide();
        mightGameComponent.Frame = new Rect(0, 70, Screen.width, Screen.height - 70 * 2);
        mightGameComponent.OnShouldClose += (view) => { return false; };
        mightGameComponent.SetSupportMultipleWindows(true);
        mightGameComponent.SetAllowBackForwardNavigationGestures(true);
        mightGameComponent.OnMultipleWindowOpened += (view, windowId) => { mightGameComponent.EmbeddedToolbar.Show(); };
        mightGameComponent.OnMultipleWindowClosed += (view, windowId) =>
        {
            mightGameComponent.EmbeddedToolbar.Hide();
        };
        mightGameComponent.OnOrientationChanged += (view, orientation) =>
        {
            mightGameComponent.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };

        mightGameComponent.OnLoadingErrorReceived += (view, code, message, payload) =>
        {
            if (payload.Extra != null &&
                payload.Extra.TryGetValue(UniWebViewNativeResultPayload.ExtraFailingURLKey, out var value))
            {
                var url = value as string;
                mightGameComponent.Load(url);
            }
        };
        mightGameComponent.Load(MightGameController.mightGameController);
        mightGameComponent.Show();
    }
}
