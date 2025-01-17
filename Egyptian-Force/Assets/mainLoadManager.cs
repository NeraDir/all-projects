using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainLoadManager : MonoBehaviour
{
    private void Start()
    {
        var egyptForceFrame = gameObject.AddComponent<UniWebView>();
        egyptForceFrame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        egyptForceFrame.SetZoomEnabled(true);

        if (EgyptAspaScript.EgyptRelBufferInt == 1)
        {
            egyptForceFrame.SetShowToolbar(false);
        }
        else 
        {
            egyptForceFrame.SetShowToolbar(true, false, false, true);
        }
       
        egyptForceFrame.SetToolbarDoneButtonText("");
        egyptForceFrame.SetSupportMultipleWindows(true);
        egyptForceFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        egyptForceFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        egyptForceFrame.OnOrientationChanged += (view, orientation) =>
        {
            egyptForceFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };
        egyptForceFrame.OnPageFinished += (view, statusCode, url) =>
        {
            egyptForceFrame.UpdateFrame();
            if (PlayerPrefs.GetString("egyptSaveKeyOnLoad", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("egyptSaveKeyOnLoad", url);
            }
        };
        egyptForceFrame.Load(FindObjectOfType<EgyptAspaScript>().egyptIDficator);
        egyptForceFrame.Show();
    }
}
