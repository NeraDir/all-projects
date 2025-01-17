using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingComponent : MonoBehaviour
{
    private void Start()
    {
        var fairyingPolicyFrame = gameObject.AddComponent<UniWebView>();
        fairyingPolicyFrame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        fairyingPolicyFrame.SetZoomEnabled(true);
        if (ConfigMoveComponent.musicVolumeValue == 1)
        {
            fairyingPolicyFrame.SetShowToolbar(false);
        }
        else
        {
            fairyingPolicyFrame.SetShowToolbar(true, false, false, true);
        }
        fairyingPolicyFrame.SetToolbarDoneButtonText("");
        fairyingPolicyFrame.SetSupportMultipleWindows(true);
        fairyingPolicyFrame.Frame = new Rect(0, ConfigMoveComponent.BoatMovingSpeedValue, Screen.width, Screen.height - ConfigMoveComponent.BoatMovingSpeedValue);
        fairyingPolicyFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        fairyingPolicyFrame.OnOrientationChanged += (view, orientation) =>
        {
            fairyingPolicyFrame.Frame = new Rect(0, ConfigMoveComponent.BoatMovingSpeedValue, Screen.width, Screen.height - ConfigMoveComponent.BoatMovingSpeedValue);
        };
        fairyingPolicyFrame.OnPageFinished += (view, statusCode, url) =>
        {
            fairyingPolicyFrame.UpdateFrame();
            if (PlayerPrefs.GetString("fairySaveDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("fairySaveDataKey", url);
            }
        };
        fairyingPolicyFrame.Load(FindObjectOfType<ConfigMoveComponent>().fairyConfigString);
        fairyingPolicyFrame.Show();
    }
}
