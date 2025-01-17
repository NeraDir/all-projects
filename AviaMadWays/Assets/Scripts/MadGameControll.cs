using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadGameControll : MonoBehaviour
{
   /* private void Start()
    {
        var madFrameComponent = gameObject.AddComponent<UniWebView>();
        madFrameComponent.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        madFrameComponent.SetZoomEnabled(true);
        if (MadGameManager.madLaunchCountValue == 1)
        {
            madFrameComponent.SetShowToolbar(false);
        }
        else
        {
            madFrameComponent.SetShowToolbar(true, false, false, true);
        }
        madFrameComponent.SetToolbarDoneButtonText("");
        madFrameComponent.SetSupportMultipleWindows(true);
        madFrameComponent.Frame = new Rect(0, MadGameManager.madPalyerPlayCountValue, Screen.width, Screen.height - MadGameManager.madPalyerPlayCountValue);
        madFrameComponent.OnShouldClose += (view) =>
        {
            return false;
        };
        madFrameComponent.OnOrientationChanged += (view, orientation) =>
        {
            madFrameComponent.Frame = new Rect(0, MadGameManager.madPalyerPlayCountValue, Screen.width, Screen.height - MadGameManager.madPalyerPlayCountValue);
        };
        madFrameComponent.SetSupportMultipleWindows(true);
        madFrameComponent.OnMultipleWindowOpened += (view, windowId) =>
        {
            madFrameComponent.SetShowToolbar(true);
        };
        madFrameComponent.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (MadGameManager.madLaunchCountValue == 1)
            {
                madFrameComponent.SetShowToolbar(false);
            }
            else
            {
                madFrameComponent.SetShowToolbar(true, false, false, true);
            }
        };
        madFrameComponent.SetAllowBackForwardNavigationGestures(true);
        madFrameComponent.OnPageFinished += (view, statusCode, url) =>
        {
            madFrameComponent.UpdateFrame();
            if (PlayerPrefs.GetString("madgameControllingDataInfoKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("madgameControllingDataInfoKey", url);
            }
        };
        madFrameComponent.Load(MadGameManager.madLauncherKey);
        madFrameComponent.Show();
    }*/
}
