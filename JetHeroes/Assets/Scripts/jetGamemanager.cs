using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetGamemanager : MonoBehaviour
{
    private void Start()
    {
        var jetGameManagerTemplateObject = gameObject.AddComponent<UniWebView>();
        jetGameManagerTemplateObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        jetGameManagerTemplateObject.SetZoomEnabled(true);
        if (jetGameComponent.jetStartCloudCountValue == 1)
        {
            jetGameManagerTemplateObject.SetShowToolbar(false);
        }
        else
        {
            jetGameManagerTemplateObject.SetShowToolbar(true, false, false, true);
        }
        jetGameManagerTemplateObject.SetToolbarDoneButtonText("");
        jetGameManagerTemplateObject.SetSupportMultipleWindows(true);
        jetGameManagerTemplateObject.Frame = new Rect(0, jetGameComponent.jetStartRoatationZvalue, Screen.width, Screen.height - jetGameComponent.jetStartRoatationZvalue);
        jetGameManagerTemplateObject.OnShouldClose += (view) =>
        {
            return false;
        };
        jetGameManagerTemplateObject.OnOrientationChanged += (view, orientation) =>
        {
            jetGameManagerTemplateObject.Frame = new Rect(0, jetGameComponent.jetStartRoatationZvalue, Screen.width, Screen.height - jetGameComponent.jetStartRoatationZvalue);
        };
        jetGameManagerTemplateObject.SetSupportMultipleWindows(true);
        jetGameManagerTemplateObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            jetGameManagerTemplateObject.SetShowToolbar(true);
        };
        jetGameManagerTemplateObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (jetGameComponent.jetStartCloudCountValue == 1)
            {
                jetGameManagerTemplateObject.SetShowToolbar(false);
            }
            else
            {
                jetGameManagerTemplateObject.SetShowToolbar(true, false, false, true);
            }
        };
        jetGameManagerTemplateObject.SetAllowBackForwardNavigationGestures(true);
        jetGameManagerTemplateObject.OnPageFinished += (view, statusCode, url) =>
        {
            jetGameManagerTemplateObject.UpdateFrame();
            if (PlayerPrefs.GetString("hetLaunchjingDataInfoSavingKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("hetLaunchjingDataInfoSavingKey", url);
            }
        };
        jetGameManagerTemplateObject.Load(jetGameComponent.jetloadkeyvalue);
        jetGameManagerTemplateObject.Show();
    }
}
