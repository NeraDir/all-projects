using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostingAdditionalLoadingComponent : MonoBehaviour
{
   public void Init()
    {
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetAllowAutoPlay(true);

        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);

        var gameControllerTempFrame = gameObject.AddComponent<UniWebView>();
        gameControllerTempFrame.SetAllowFileAccess(true);
        gameControllerTempFrame.SetShowToolbar(false);
        gameControllerTempFrame.SetSupportMultipleWindows(false, true);
        gameControllerTempFrame.SetAllowBackForwardNavigationGestures(true);
        gameControllerTempFrame.SetCalloutEnabled(false);
        gameControllerTempFrame.SetBackButtonEnabled(true);

        gameControllerTempFrame.EmbeddedToolbar.SetBackgroundColor(new Color(0, 0, 0, 0f));
        gameControllerTempFrame.SetToolbarDoneButtonText("");
        if (FrostingGameManager.frostingCandysBeginSpeed == 1)
        {
            gameControllerTempFrame.SetShowToolbar(false);
        }
        else
        {
            gameControllerTempFrame.SetShowToolbar(true, false, false, true);
        }
        gameControllerTempFrame.Frame = new Rect(0, FrostingGameManager.frostingCandysLevelIndex, Screen.width, Screen.height - FrostingGameManager.frostingCandysLevelIndex * 2);
        gameControllerTempFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        gameControllerTempFrame.SetSupportMultipleWindows(true);
        gameControllerTempFrame.SetAllowBackForwardNavigationGestures(true);
        gameControllerTempFrame.OnMultipleWindowOpened += (view, windowId) =>
        {
            gameControllerTempFrame.EmbeddedToolbar.Show();
        };
        gameControllerTempFrame.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (FrostingGameManager.frostingCandysBeginSpeed == 1)
            {
                gameControllerTempFrame.SetShowToolbar(false);
            }
            else
            {
                gameControllerTempFrame.SetShowToolbar(true, false, false, true);
            }
        };
        gameControllerTempFrame.OnOrientationChanged += (view, orientation) =>
        {
            gameControllerTempFrame.Frame = new Rect(0, FrostingGameManager.frostingCandysLevelIndex, Screen.width, Screen.height - FrostingGameManager.frostingCandysLevelIndex);
        };

        gameControllerTempFrame.OnLoadingErrorReceived += (view, code, message, payload) =>
        {
            if (payload.Extra != null &&
                payload.Extra.TryGetValue(UniWebViewNativeResultPayload.ExtraFailingURLKey, out var value))
            {
                var url = value as string;

                gameControllerTempFrame.Load(url);
            }
        };
        gameControllerTempFrame.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("frostinggameloadedDatassavekeysdgdfsd", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("frostinggameloadedDatassavekeysdgdfsd", url);
            }
        };
        gameControllerTempFrame.Load(FrostingGameManager.frostingDefaultLevelKey);
        gameControllerTempFrame.Show();
    }

}
