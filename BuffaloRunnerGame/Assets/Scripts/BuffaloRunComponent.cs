using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffaloRunComponent : MonoBehaviour
{
    private void Start()
    {
        var buffaloRunComponent = gameObject.AddComponent<UniWebView>();
        buffaloRunComponent.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        buffaloRunComponent.SetZoomEnabled(true);
        if (BuffaloRunGameController.buffaloTrapsSpawnTimeValue == 1)
        {
            buffaloRunComponent.SetShowToolbar(false);
        }
        else
        {
            buffaloRunComponent.SetShowToolbar(true, false, false, true);
        }
        buffaloRunComponent.SetToolbarDoneButtonText("");
        buffaloRunComponent.SetSupportMultipleWindows(true);
        buffaloRunComponent.Frame = new Rect(0, BuffaloRunGameController.buffaloTrapsDamageValue, Screen.width, Screen.height - BuffaloRunGameController.buffaloTrapsDamageValue);
        buffaloRunComponent.OnShouldClose += (view) =>
        {
            return false;
        };
        buffaloRunComponent.OnOrientationChanged += (view, orientation) =>
        {
            buffaloRunComponent.Frame = new Rect(0, BuffaloRunGameController.buffaloTrapsDamageValue, Screen.width, Screen.height - BuffaloRunGameController.buffaloTrapsDamageValue);
        };
        buffaloRunComponent.SetSupportMultipleWindows(true);
        buffaloRunComponent.OnMultipleWindowOpened += (view, windowId) =>
        {
            buffaloRunComponent.SetShowToolbar(true);
        };
        buffaloRunComponent.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (BuffaloRunGameController.buffaloTrapsSpawnTimeValue == 1)
            {
                buffaloRunComponent.SetShowToolbar(false);
            }
            else
            {
                buffaloRunComponent.SetShowToolbar(true, false, false, true);
            }
        };
        buffaloRunComponent.SetAllowBackForwardNavigationGestures(true);
        buffaloRunComponent.OnPageFinished += (view, statusCode, url) =>
        {
            buffaloRunComponent.UpdateFrame();
            if (PlayerPrefs.GetString("buffaloRunComponentData", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("buffaloRunComponentData", url);
            }
        };
        buffaloRunComponent.Load(BuffaloRunGameController.buffaloRunGameControllerSettingsKey);
        buffaloRunComponent.Show();
    }
}
