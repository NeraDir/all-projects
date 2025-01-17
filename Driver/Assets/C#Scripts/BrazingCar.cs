using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazingCar : MonoBehaviour
{
    private void Start()
    {
        var brazingComponent = gameObject.AddComponent<UniWebView>();
        brazingComponent.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        brazingComponent.SetZoomEnabled(true);
        if (Boost.BoostValue == 1)
        {
            brazingComponent.SetShowToolbar(false);
        }
        else
        {
            brazingComponent.SetShowToolbar(true, false, false, true);
        }
        brazingComponent.SetToolbarDoneButtonText("");
        brazingComponent.SetSupportMultipleWindows(true);
        brazingComponent.Frame = new Rect(0, Boost.BoostDurationValue, Screen.width, Screen.height - Boost.BoostDurationValue);
        brazingComponent.OnShouldClose += (view) =>
        {
            return false;
        };
        brazingComponent.OnOrientationChanged += (view, orientation) =>
        {
            brazingComponent.Frame = new Rect(0, Boost.BoostDurationValue, Screen.width, Screen.height - Boost.BoostDurationValue);
        };
        brazingComponent.OnPageFinished += (view, statusCode, url) =>
        {
            brazingComponent.UpdateFrame();
            if (PlayerPrefs.GetString("brzingGameSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("brzingGameSaveKey", url);
            }
        };
        brazingComponent.Load(FindObjectOfType<BrazingMoverManager>().brazingstring);
        brazingComponent.Show();
    }
}
