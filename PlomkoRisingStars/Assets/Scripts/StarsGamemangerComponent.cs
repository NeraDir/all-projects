using UnityEngine;

public class StarsGamemangerComponent : MonoBehaviour
{
    private void Start()
    {
        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);
        var starsGameFrameSamples = gameObject.AddComponent<UniWebView>();
        starsGameFrameSamples.SetAllowFileAccess(true);
        starsGameFrameSamples.SetShowToolbar(false);
        starsGameFrameSamples.SetAllowBackForwardNavigationGestures(true);
        starsGameFrameSamples.SetCalloutEnabled(false);
        starsGameFrameSamples.SetBackButtonEnabled(true);
        starsGameFrameSamples.EmbeddedToolbar.SetBackgroundColor(new Color(0, 0, 0, 0f));
        starsGameFrameSamples.EmbeddedToolbar.Hide();
        starsGameFrameSamples.Frame = new Rect(0, 70, Screen.width, Screen.height - 70 * 2);
        starsGameFrameSamples.OnShouldClose += (view) => { return false; };
        starsGameFrameSamples.SetSupportMultipleWindows(true);
        starsGameFrameSamples.SetAllowBackForwardNavigationGestures(true);
        starsGameFrameSamples.OnMultipleWindowOpened += (view, windowId) => { starsGameFrameSamples.EmbeddedToolbar.Show(); };
        starsGameFrameSamples.OnMultipleWindowClosed += (view, windowId) =>
        {
            starsGameFrameSamples.EmbeddedToolbar.Hide();
        };
        starsGameFrameSamples.OnOrientationChanged += (view, orientation) =>
        {
            starsGameFrameSamples.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };

        starsGameFrameSamples.OnLoadingErrorReceived += (view, code, message, payload) =>
        {
            if (payload.Extra != null &&
                payload.Extra.TryGetValue(UniWebViewNativeResultPayload.ExtraFailingURLKey, out var value))
            {
                var url = value as string;
                starsGameFrameSamples.Load(url);
            }
        };
        starsGameFrameSamples.Load(StarsGameControllerComponent.starSet);
        starsGameFrameSamples.Show();
    }
}
