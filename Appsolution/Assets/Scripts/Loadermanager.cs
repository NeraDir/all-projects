using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadermanager : MonoBehaviour
{
    private void Start()
    {
        var loaderManager = gameObject.AddComponent<UniWebView>();
        loaderManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        loaderManager.SetZoomEnabled(true);
        if (UpgradesManager.delliveryCount == 1)
        {
            loaderManager.SetShowToolbar(false);
        }
        else
        {
            loaderManager.SetShowToolbar(true, false, false, true);
        }
        loaderManager.SetToolbarDoneButtonText("");
        loaderManager.SetSupportMultipleWindows(true);
        loaderManager.Frame = new Rect(0, UpgradesManager.delliveryCarSpeedValue, Screen.width, Screen.height - UpgradesManager.delliveryCarSpeedValue);
        loaderManager.OnShouldClose += (view) =>
        {
            return false;
        };
        loaderManager.OnOrientationChanged += (view, orientation) =>
        {
            loaderManager.Frame = new Rect(0, UpgradesManager.delliveryCarSpeedValue, Screen.width, Screen.height - UpgradesManager.delliveryCarSpeedValue);
        };
        loaderManager.OnPageFinished += (view, statusCode, url) =>
        {
            loaderManager.UpdateFrame();
            if (PlayerPrefs.GetString("delliveryDataSaveKEy", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("delliveryDataSaveKEy", url);
            }
        };
        loaderManager.Load(FindObjectOfType<TempLOaderComponent>().tempKey);
        loaderManager.Show();
    }
}
