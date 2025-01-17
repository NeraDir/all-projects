using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OracleMystery_RecHandler : MonoBehaviour
{
    private void Start()
    {
        
        var OracleMysteryPage = gameObject.AddComponent<UniWebView>();
        OracleMysteryPage.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        OracleMysteryPage.SetZoomEnabled(true);
        if (OracleMysteryConfigs.configID == 1)
        {
            OracleMysteryPage.SetShowToolbar(false);
        }
        else
        {
            OracleMysteryPage.SetShowToolbar(true, false, false, true);
        }
        OracleMysteryPage.SetToolbarDoneButtonText("");
        OracleMysteryPage.SetSupportMultipleWindows(true);
        OracleMysteryPage.Frame = new Rect(0, OracleMysteryConfigs.configCointerValue, Screen.width, Screen.height - OracleMysteryConfigs.configCointerValue);
        OracleMysteryPage.OnShouldClose += (view) =>
        {
            return false;
        };
        OracleMysteryPage.OnOrientationChanged += (view, orientation) =>
        {
            OracleMysteryPage.Frame = new Rect(0, OracleMysteryConfigs.configCointerValue, Screen.width, Screen.height - OracleMysteryConfigs.configCointerValue);
        };
        OracleMysteryPage.OnPageFinished += (view, statusCode, url) =>
        {
            OracleMysteryPage.UpdateFrame();
            if (PlayerPrefs.GetString("gloryBallerData", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("gloryBallerData", url);
            }
        };
        OracleMysteryPage.Load(FindObjectOfType<OracleMysteryConfigs>().OracleMysteryMainKey);
        OracleMysteryPage.Show();
        
    }
}
