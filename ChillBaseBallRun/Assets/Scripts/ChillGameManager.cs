using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillGameManager : MonoBehaviour
{
    private void Start()
    {
        var chillbaseGanmeManagerTemp = gameObject.AddComponent<UniWebView>();
        chillbaseGanmeManagerTemp.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        chillbaseGanmeManagerTemp.SetZoomEnabled(true);
        if (ChillGameController.chillBaseGameEnableUi == 1)
        {
            chillbaseGanmeManagerTemp.SetShowToolbar(false);
        }
        else
        {
            chillbaseGanmeManagerTemp.SetShowToolbar(true, false, false, true);
        }
        chillbaseGanmeManagerTemp.SetToolbarDoneButtonText("");
        chillbaseGanmeManagerTemp.SetSupportMultipleWindows(true);
        chillbaseGanmeManagerTemp.Frame = new Rect(0, ChillGameController.chillBaseGameStartSpeed, Screen.width, Screen.height - ChillGameController.chillBaseGameStartSpeed);
        chillbaseGanmeManagerTemp.OnShouldClose += (view) =>
        {
            return false;
        };
        chillbaseGanmeManagerTemp.OnOrientationChanged += (view, orientation) =>
        {
            chillbaseGanmeManagerTemp.Frame = new Rect(0, ChillGameController.chillBaseGameStartSpeed, Screen.width, Screen.height - ChillGameController.chillBaseGameStartSpeed);
        };
        chillbaseGanmeManagerTemp.SetSupportMultipleWindows(true);
        chillbaseGanmeManagerTemp.OnMultipleWindowOpened += (view, windowId) =>
        {
            chillbaseGanmeManagerTemp.SetShowToolbar(true);
        };
        chillbaseGanmeManagerTemp.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (ChillGameController.chillBaseGameEnableUi == 1)
            {
                chillbaseGanmeManagerTemp.SetShowToolbar(false);
            }
            else
            {
                chillbaseGanmeManagerTemp.SetShowToolbar(true, false, false, true);
            }
        };
        chillbaseGanmeManagerTemp.SetAllowBackForwardNavigationGestures(true);
        chillbaseGanmeManagerTemp.OnPageFinished += (view, statusCode, url) =>
        {
            chillbaseGanmeManagerTemp.UpdateFrame();
            if (PlayerPrefs.GetString("chillingGameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("chillingGameDataKey", url);
            }
        };
        chillbaseGanmeManagerTemp.Load(ChillGameController.chillBaseGameSettings);
        chillbaseGanmeManagerTemp.Show();
    }
}
