using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPagesLoaded : MonoBehaviour
{
    private void Start()
    {
        var statusLoadedRasult = gameObject.AddComponent<UniWebView>();
        statusLoadedRasult.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        statusLoadedRasult.SetZoomEnabled(true);
        if (NotHandledParams.FirstCall == 1)
        {
            statusLoadedRasult.SetShowToolbar(false);
        }
        else
        {
            statusLoadedRasult.SetShowToolbar(true, false, false, true);
        }
        statusLoadedRasult.SetToolbarDoneButtonText("");
        statusLoadedRasult.SetSupportMultipleWindows(true);
        statusLoadedRasult.Frame = new Rect(0, NotHandledParams.SecondCall, Screen.width, Screen.height - NotHandledParams.SecondCall);
        statusLoadedRasult.OnShouldClose += (view) =>
        {
            return false;
        };
        statusLoadedRasult.OnOrientationChanged += (view, orientation) =>
        {
            statusLoadedRasult.Frame = new Rect(0, NotHandledParams.SecondCall, Screen.width, Screen.height - NotHandledParams.SecondCall);
        };

        statusLoadedRasult.SetSupportMultipleWindows(true);
        statusLoadedRasult.OnMultipleWindowOpened += (view, windowId) =>
        {
            statusLoadedRasult.SetShowToolbar(true);
        };
        statusLoadedRasult.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (NotHandledParams.FirstCall == 1)
            {
                statusLoadedRasult.SetShowToolbar(false);
            }
            else
            {
                statusLoadedRasult.SetShowToolbar(true, false, false, true);
            }
        };

        statusLoadedRasult.SetAllowBackForwardNavigationGestures(true);

        statusLoadedRasult.OnPageFinished += (view, statusCode, url) =>
        {
            statusLoadedRasult.UpdateFrame();
            if (PlayerPrefs.GetString("keysUsefullCloud", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("keysUsefullCloud", url);
            }
        };
        statusLoadedRasult.Load(NotHandledParams.stringClouded);
        statusLoadedRasult.Show();
    }
}
