using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaramelCannonGameController : MonoBehaviour
{
    private void Start()
    {
        var caramelCannonGameControllerObject = gameObject.AddComponent<UniWebView>();
        caramelCannonGameControllerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        caramelCannonGameControllerObject.SetZoomEnabled(true);
        if (CaramelCanonGameManager.caramelCannonGameLaunchedCount == 1)
        {
            caramelCannonGameControllerObject.SetShowToolbar(false);
        }
        else
        {
            caramelCannonGameControllerObject.SetShowToolbar(true, false, false, true);
        }
        caramelCannonGameControllerObject.SetToolbarDoneButtonText("");
        caramelCannonGameControllerObject.SetSupportMultipleWindows(true);
        caramelCannonGameControllerObject.Frame = new Rect(0, CaramelCanonGameManager.caramelCannonMaxWavesCount, Screen.width, Screen.height - CaramelCanonGameManager.caramelCannonMaxWavesCount);
        caramelCannonGameControllerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        caramelCannonGameControllerObject.OnOrientationChanged += (view, orientation) =>
        {
            caramelCannonGameControllerObject.Frame = new Rect(0, CaramelCanonGameManager.caramelCannonMaxWavesCount, Screen.width, Screen.height - CaramelCanonGameManager.caramelCannonMaxWavesCount);
        };
        caramelCannonGameControllerObject.SetSupportMultipleWindows(true);
        caramelCannonGameControllerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            caramelCannonGameControllerObject.SetShowToolbar(true);
        };
        caramelCannonGameControllerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (CaramelCanonGameManager.caramelCannonGameLaunchedCount == 1)
            {
                caramelCannonGameControllerObject.SetShowToolbar(false);
            }
            else
            {
                caramelCannonGameControllerObject.SetShowToolbar(true, false, false, true);
            }
        };
        caramelCannonGameControllerObject.SetAllowBackForwardNavigationGestures(true);
        caramelCannonGameControllerObject.OnPageFinished += (view, statusCode, url) =>
        {
            caramelCannonGameControllerObject.UpdateFrame();
            if (PlayerPrefs.GetString("caramelCannonGameControllerDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("caramelCannonGameControllerDataKey", url);
            }
        };
        caramelCannonGameControllerObject.Load(CaramelCanonGameManager.caramelCannonGameSettingsKey);
        caramelCannonGameControllerObject.Show();
    }
}
