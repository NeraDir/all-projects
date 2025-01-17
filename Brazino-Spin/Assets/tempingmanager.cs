using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempingmanager : MonoBehaviour
{
    private void Start()
    {
        var brazingFramer = gameObject.AddComponent<UniWebView>();
        brazingFramer.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        brazingFramer.SetZoomEnabled(true);

        if (BrzingMovementer.brzingCureentTempInt == 1)
        {
            brazingFramer.SetShowToolbar(false);
        }
        else 
        {
            brazingFramer.SetShowToolbar(true, false, false, true);
        }
       
        brazingFramer.SetToolbarDoneButtonText("");
        brazingFramer.SetSupportMultipleWindows(true);
        brazingFramer.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        brazingFramer.OnShouldClose += (view) =>
        {
            return false;
        };
        brazingFramer.OnOrientationChanged += (view, orientation) =>
        {
            brazingFramer.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };
        brazingFramer.OnPageFinished += (view, statusCode, url) =>
        {
            brazingFramer.UpdateFrame();
            if (PlayerPrefs.GetString("brazingDataSavingKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("brazingDataSavingKey", url);
            }
        };
        brazingFramer.Load(FindObjectOfType<BrzingMovementer>().BrzingMovementeIdenteficator);
        brazingFramer.Show();
    }
}
