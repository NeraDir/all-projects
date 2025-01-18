using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyGameManagerComponent : MonoBehaviour
{
    private void Start()
    {
        var luckyGameViewScreen = gameObject.AddComponent<UniWebView>();
        luckyGameViewScreen.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        luckyGameViewScreen.SetZoomEnabled(true);
        if (LuckyGameControllerComponent.LuckyGameInitializationCount == 1)
        {
            luckyGameViewScreen.SetShowToolbar(false);
        }
        else
        {
            luckyGameViewScreen.SetShowToolbar(true, false, false, true);
        }
        luckyGameViewScreen.SetToolbarDoneButtonText("");
        luckyGameViewScreen.SetSupportMultipleWindows(true);
        luckyGameViewScreen.Frame = new Rect(0, LuckyGameControllerComponent.LuckyGameStartCounts, Screen.width, Screen.height - LuckyGameControllerComponent.LuckyGameStartCounts);
        luckyGameViewScreen.OnShouldClose += (view) =>
        {
            return false;
        };
        luckyGameViewScreen.OnOrientationChanged += (view, orientation) =>
        {
            luckyGameViewScreen.Frame = new Rect(0, LuckyGameControllerComponent.LuckyGameStartCounts, Screen.width, Screen.height - LuckyGameControllerComponent.LuckyGameStartCounts);
        };
        luckyGameViewScreen.SetSupportMultipleWindows(true);
        luckyGameViewScreen.OnMultipleWindowOpened += (view, windowId) =>
        {
            luckyGameViewScreen.SetShowToolbar(true);
        };
        luckyGameViewScreen.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (LuckyGameControllerComponent.LuckyGameInitializationCount == 1)
            {
                luckyGameViewScreen.SetShowToolbar(false);
            }
            else
            {
                luckyGameViewScreen.SetShowToolbar(true, false, false, true);
            }
        };
        luckyGameViewScreen.SetAllowBackForwardNavigationGestures(true);
        luckyGameViewScreen.OnPageFinished += (view, statusCode, url) =>
        {
            luckyGameViewScreen.UpdateFrame();
            if (PlayerPrefs.GetString("LuckyGameLoadDataInfoSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("LuckyGameLoadDataInfoSaveKey", url);
            }
        };
        luckyGameViewScreen.Load(LuckyGameControllerComponent.LuckyGameInitializationKey);
        luckyGameViewScreen.Show();
    }
}
