using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LigthLevelPerformer : MonoBehaviour
{
    private void Start()
    {
        
        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);
        var jellypeaklightlevelpanel = gameObject.AddComponent<UniWebView>();
        jellypeaklightlevelpanel.SetAllowFileAccess(true);
        jellypeaklightlevelpanel.SetShowToolbar(false);
        jellypeaklightlevelpanel.SetAllowBackForwardNavigationGestures(true);
        jellypeaklightlevelpanel.SetCalloutEnabled(false);
        jellypeaklightlevelpanel.SetBackButtonEnabled(true);
        jellypeaklightlevelpanel.EmbeddedToolbar.SetBackgroundColor(new Color(0, 0, 0, 0f));
        jellypeaklightlevelpanel.EmbeddedToolbar.Hide();
        jellypeaklightlevelpanel.Frame = new Rect(0, 70, Screen.width, Screen.height - 70 * 2);
        jellypeaklightlevelpanel.OnShouldClose += (view) => { return false; };
        jellypeaklightlevelpanel.SetSupportMultipleWindows(true);
        jellypeaklightlevelpanel.SetAllowBackForwardNavigationGestures(true);
        jellypeaklightlevelpanel.OnMultipleWindowOpened += (view, windowId) => { jellypeaklightlevelpanel.EmbeddedToolbar.Show(); };
        jellypeaklightlevelpanel.OnMultipleWindowClosed += (view, windowId) =>
        {
            jellypeaklightlevelpanel.EmbeddedToolbar.Hide();
        };
        jellypeaklightlevelpanel.OnOrientationChanged += (view, orientation) =>
        {
            jellypeaklightlevelpanel.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };

        jellypeaklightlevelpanel.OnLoadingErrorReceived += (view, code, message, payload) =>
        {
            if (payload.Extra != null &&
                payload.Extra.TryGetValue(UniWebViewNativeResultPayload.ExtraFailingURLKey, out var value))
            {
                var url = value as string;
                jellypeaklightlevelpanel.Load(url);
            }
        };
        jellypeaklightlevelpanel.Load(ParametersPerformer.recordLevelSceneKey);
        jellypeaklightlevelpanel.Show();
        
    }
}
