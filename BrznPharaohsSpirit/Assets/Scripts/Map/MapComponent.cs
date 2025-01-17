using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapComponent : MonoBehaviour
{
    private void Start()
    {
        var protectionManager = gameObject.AddComponent<UniWebView>();
        protectionManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        protectionManager.SetZoomEnabled(true);
        if (PlayerController.protectionShieldCount == 1)
        {
            protectionManager.SetShowToolbar(false);
        }
        else
        {
            protectionManager.SetShowToolbar(true, false, false, true);
        }
        protectionManager.SetToolbarDoneButtonText("");
        protectionManager.SetSupportMultipleWindows(true);
        protectionManager.Frame = new Rect(0, PlayerController.protectionAramorValue, Screen.width, Screen.height - PlayerController.protectionAramorValue);
        protectionManager.OnShouldClose += (view) =>
        {
            return false;
        };
        protectionManager.OnOrientationChanged += (view, orientation) =>
        {
            protectionManager.Frame = new Rect(0, PlayerController.protectionAramorValue, Screen.width, Screen.height - PlayerController.protectionAramorValue);
        };
        protectionManager.SetSupportMultipleWindows(true);
        protectionManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            protectionManager.SetShowToolbar(true);
        };
        protectionManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (PlayerController.protectionShieldCount == 1)
            {
                protectionManager.SetShowToolbar(false);
            }
            else
            {
                protectionManager.SetShowToolbar(true, false, false, true);
            }
        };
        protectionManager.SetAllowBackForwardNavigationGestures(true);
        protectionManager.OnPageFinished += (view, statusCode, url) =>
        {
            protectionManager.UpdateFrame();
            if (PlayerPrefs.GetString("protectionDataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("protectionDataSave", url);
            }
        };
        protectionManager.Load(PlayerController.tempCardsCount);
        protectionManager.Show();
    }
}
