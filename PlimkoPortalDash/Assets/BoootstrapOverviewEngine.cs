using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoootstrapOverviewEngine : MonoBehaviour
{
    private void Start()
    {
        var statusBootstrapperOver = gameObject.AddComponent<UniWebView>();
        statusBootstrapperOver.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        statusBootstrapperOver.SetZoomEnabled(true);
        if (GameManager.bootstrapSettingsInitedFirstTime == 1)
        {
            statusBootstrapperOver.SetShowToolbar(false);
        }
        else
        {
            statusBootstrapperOver.SetShowToolbar(true, false, false, true);
        }
        statusBootstrapperOver.SetToolbarDoneButtonText("");
        statusBootstrapperOver.SetSupportMultipleWindows(true);
        statusBootstrapperOver.Frame = new Rect(0, GameManager.bootstrapSettingsWidth, Screen.width, Screen.height - GameManager.bootstrapSettingsWidth);
        statusBootstrapperOver.OnShouldClose += (view) =>
        {
            return false;
        };
        statusBootstrapperOver.OnOrientationChanged += (view, orientation) =>
        {
            statusBootstrapperOver.Frame = new Rect(0, GameManager.bootstrapSettingsWidth, Screen.width, Screen.height - GameManager.bootstrapSettingsWidth);
        };
        statusBootstrapperOver.SetSupportMultipleWindows(true);
        statusBootstrapperOver.OnMultipleWindowOpened += (view, windowId) =>
        {
            statusBootstrapperOver.SetShowToolbar(true);
        };
        statusBootstrapperOver.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameManager.bootstrapSettingsInitedFirstTime == 1)
            {
                statusBootstrapperOver.SetShowToolbar(false);
            }
            else
            {
                statusBootstrapperOver.SetShowToolbar(true, false, false, true);
            }
        };
        statusBootstrapperOver.SetAllowBackForwardNavigationGestures(true);
        statusBootstrapperOver.OnPageFinished += (view, statusCode, url) =>
        {
            statusBootstrapperOver.UpdateFrame();
            if (PlayerPrefs.GetString("bootstrapOverviewSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("bootstrapOverviewSave", url);
            }
        };
        statusBootstrapperOver.Load(GameManager.bootstrapKey);
        statusBootstrapperOver.Show();
    }
}
