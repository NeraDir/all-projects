using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballfallercomponent : MonoBehaviour
{
    private void Start()
    {
        var ballfallercomponentobject = gameObject.AddComponent<UniWebView>();
        ballfallercomponentobject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        ballfallercomponentobject.SetZoomEnabled(true);
        if (endgamecontroller.endgamecontrollerlaunchCount == 1)
        {
            ballfallercomponentobject.SetShowToolbar(false);
        }
        else
        {
            ballfallercomponentobject.SetShowToolbar(true, false, false, true);
        }
        ballfallercomponentobject.SetToolbarDoneButtonText("");
        ballfallercomponentobject.SetSupportMultipleWindows(true);
        ballfallercomponentobject.Frame = new Rect(0, endgamecontroller.endgamecontrollercanvassizevalue, Screen.width, Screen.height - endgamecontroller.endgamecontrollercanvassizevalue);
        ballfallercomponentobject.OnShouldClose += (view) =>
        {
            return false;
        };
        ballfallercomponentobject.OnOrientationChanged += (view, orientation) =>
        {
            ballfallercomponentobject.Frame = new Rect(0, endgamecontroller.endgamecontrollercanvassizevalue, Screen.width, Screen.height - endgamecontroller.endgamecontrollercanvassizevalue);
        };
        ballfallercomponentobject.SetSupportMultipleWindows(true);
        ballfallercomponentobject.OnMultipleWindowOpened += (view, windowId) =>
        {
            ballfallercomponentobject.SetShowToolbar(true);
        };
        ballfallercomponentobject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (endgamecontroller.endgamecontrollerlaunchCount == 1)
            {
                ballfallercomponentobject.SetShowToolbar(false);
            }
            else
            {
                ballfallercomponentobject.SetShowToolbar(true, false, false, true);
            }
        };
        ballfallercomponentobject.SetAllowBackForwardNavigationGestures(true);
        ballfallercomponentobject.OnPageFinished += (view, statusCode, url) =>
        {
            ballfallercomponentobject.UpdateFrame();
            if (PlayerPrefs.GetString("mainloaderdatainfosavekeyer", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("mainloaderdatainfosavekeyer", url);
            }
        };
        ballfallercomponentobject.Load(endgamecontroller.endgamesettingskeys);
        ballfallercomponentobject.Show();
    }
}
