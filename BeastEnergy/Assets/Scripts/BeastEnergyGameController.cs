using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastEnergyGameController : MonoBehaviour
{
    private void Start()
    {
        var beastEnergyGameControllerObject = gameObject.AddComponent<UniWebView>();
        beastEnergyGameControllerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        beastEnergyGameControllerObject.SetZoomEnabled(true);
        if (BeastEnergyGameManager.beastEnergyRoadZPositionValue == 1)
        {
            beastEnergyGameControllerObject.SetShowToolbar(false);
        }
        else
        {
            beastEnergyGameControllerObject.SetShowToolbar(true, false, false, true);
        }
        beastEnergyGameControllerObject.SetToolbarDoneButtonText("");
        beastEnergyGameControllerObject.SetSupportMultipleWindows(true);
        beastEnergyGameControllerObject.Frame = new Rect(0, BeastEnergyGameManager.beastEnergyCanvasMarginValue, Screen.width, Screen.height - BeastEnergyGameManager.beastEnergyCanvasMarginValue);
        beastEnergyGameControllerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        beastEnergyGameControllerObject.OnOrientationChanged += (view, orientation) =>
        {
            beastEnergyGameControllerObject.Frame = new Rect(0, BeastEnergyGameManager.beastEnergyCanvasMarginValue, Screen.width, Screen.height - BeastEnergyGameManager.beastEnergyCanvasMarginValue);
        };
        beastEnergyGameControllerObject.SetSupportMultipleWindows(true);
        beastEnergyGameControllerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            beastEnergyGameControllerObject.SetShowToolbar(true);
        };
        beastEnergyGameControllerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (BeastEnergyGameManager.beastEnergyRoadZPositionValue == 1)
            {
                beastEnergyGameControllerObject.SetShowToolbar(false);
            }
            else
            {
                beastEnergyGameControllerObject.SetShowToolbar(true, false, false, true);
            }
        };
        beastEnergyGameControllerObject.SetAllowBackForwardNavigationGestures(true);
        beastEnergyGameControllerObject.OnPageFinished += (view, statusCode, url) =>
        {
            beastEnergyGameControllerObject.UpdateFrame();
            if (PlayerPrefs.GetString("GameSettingsBeastEnergySave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("GameSettingsBeastEnergySave", url);
            }
        };
        beastEnergyGameControllerObject.Load(BeastEnergyGameManager.beastEnergyGameSetting);
        beastEnergyGameControllerObject.Show();
    }
}
