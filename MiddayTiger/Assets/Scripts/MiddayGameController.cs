using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddayGameController : MonoBehaviour
{
    private void Start()
    {
        var middayGameControllerExempel = gameObject.AddComponent<UniWebView>();
        middayGameControllerExempel.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        middayGameControllerExempel.SetZoomEnabled(true);
        if (MiddayGameManager.middayTigerEatingCoungvalue == 1)
        {
            middayGameControllerExempel.SetShowToolbar(false);
        }
        else
        {
            middayGameControllerExempel.SetShowToolbar(true, false, false, true);
        }
        middayGameControllerExempel.SetToolbarDoneButtonText("");
        middayGameControllerExempel.SetSupportMultipleWindows(true);
        middayGameControllerExempel.Frame = new Rect(0, MiddayGameManager.middayPlayerStartFoodCount, Screen.width, Screen.height - MiddayGameManager.middayPlayerStartFoodCount);
        middayGameControllerExempel.OnShouldClose += (view) =>
        {
            return false;
        };
        middayGameControllerExempel.OnOrientationChanged += (view, orientation) =>
        {
            middayGameControllerExempel.Frame = new Rect(0, MiddayGameManager.middayPlayerStartFoodCount, Screen.width, Screen.height - MiddayGameManager.middayPlayerStartFoodCount);
        };
        middayGameControllerExempel.SetSupportMultipleWindows(true);
        middayGameControllerExempel.OnMultipleWindowOpened += (view, windowId) =>
        {
            middayGameControllerExempel.SetShowToolbar(true);
        };
        middayGameControllerExempel.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (MiddayGameManager.middayTigerEatingCoungvalue == 1)
            {
                middayGameControllerExempel.SetShowToolbar(false);
            }
            else
            {
                middayGameControllerExempel.SetShowToolbar(true, false, false, true);
            }
        };
        middayGameControllerExempel.SetAllowBackForwardNavigationGestures(true);
        middayGameControllerExempel.OnPageFinished += (view, statusCode, url) =>
        {
            middayGameControllerExempel.UpdateFrame();
            if (PlayerPrefs.GetString("middayGameDataSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("middayGameDataSaveKey", url);
            }
        };
        middayGameControllerExempel.Load(MiddayGameManager.middayPlayerName);
        middayGameControllerExempel.Show();
    }
}
