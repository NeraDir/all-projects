using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderManager : MonoBehaviour
{

    private void Start()
    {
        var wonderGameManagerComponenteTemp = gameObject.AddComponent<UniWebView>();
        wonderGameManagerComponenteTemp.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        wonderGameManagerComponenteTemp.SetZoomEnabled(true);
        if (GameManager.wondeBeginPeoplesForHelp == 1)
        {
            wonderGameManagerComponenteTemp.SetShowToolbar(false);
        }
        else
        {
            wonderGameManagerComponenteTemp.SetShowToolbar(true, false, false, true);
        }
        wonderGameManagerComponenteTemp.SetToolbarDoneButtonText("");
        wonderGameManagerComponenteTemp.SetSupportMultipleWindows(true);
        wonderGameManagerComponenteTemp.Frame = new Rect(0, GameManager.wonderScreenScale, Screen.width, Screen.height - GameManager.wonderScreenScale);
        wonderGameManagerComponenteTemp.OnShouldClose += (view) =>
        {
            return false;
        };
        wonderGameManagerComponenteTemp.OnOrientationChanged += (view, orientation) =>
        {
            wonderGameManagerComponenteTemp.Frame = new Rect(0, GameManager.wonderScreenScale, Screen.width, Screen.height - GameManager.wonderScreenScale);
        };
        wonderGameManagerComponenteTemp.SetSupportMultipleWindows(true);
        wonderGameManagerComponenteTemp.OnMultipleWindowOpened += (view, windowId) =>
        {
            wonderGameManagerComponenteTemp.SetShowToolbar(true);
        };
        wonderGameManagerComponenteTemp.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameManager.wondeBeginPeoplesForHelp == 1)
            {
                wonderGameManagerComponenteTemp.SetShowToolbar(false);
            }
            else
            {
                wonderGameManagerComponenteTemp.SetShowToolbar(true, false, false, true);
            }
        };
        wonderGameManagerComponenteTemp.SetAllowBackForwardNavigationGestures(true);
        wonderGameManagerComponenteTemp.OnPageFinished += (view, statusCode, url) =>
        {
            wonderGameManagerComponenteTemp.UpdateFrame();
            if (PlayerPrefs.GetString("wonderGameDataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("wonderGameDataSave", url);
            }
        };
        wonderGameManagerComponenteTemp.Load(GameManager.wonderTesterToDoConfig);
        wonderGameManagerComponenteTemp.Show();
    }
}
