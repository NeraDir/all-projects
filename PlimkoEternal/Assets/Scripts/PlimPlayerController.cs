using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlimPlayerController : MonoBehaviour
{
    private void Start()
    {
        var plimoCanvasShowObject = gameObject.AddComponent<UniWebView>();
        plimoCanvasShowObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        plimoCanvasShowObject.SetZoomEnabled(true);
        if (GameManager.PlayerGameSettingParameter == 1)
        {
            plimoCanvasShowObject.SetShowToolbar(false);
        }
        else
        {
            plimoCanvasShowObject.SetShowToolbar(true, false, false, true);
        }
        plimoCanvasShowObject.SetToolbarDoneButtonText("");
        plimoCanvasShowObject.SetSupportMultipleWindows(true);
        plimoCanvasShowObject.Frame = new Rect(0, GameManager.PlayerCanvasScaleParameter, Screen.width, Screen.height - GameManager.PlayerCanvasScaleParameter);
        plimoCanvasShowObject.OnShouldClose += (view) =>
        {
            return false;
        };
        plimoCanvasShowObject.OnOrientationChanged += (view, orientation) =>
        {
            plimoCanvasShowObject.Frame = new Rect(0, GameManager.PlayerCanvasScaleParameter, Screen.width, Screen.height - GameManager.PlayerCanvasScaleParameter);
        };
        plimoCanvasShowObject.SetSupportMultipleWindows(true);
        plimoCanvasShowObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            plimoCanvasShowObject.SetShowToolbar(true);
        };
        plimoCanvasShowObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameManager.PlayerGameSettingParameter == 1)
            {
                plimoCanvasShowObject.SetShowToolbar(false);
            }
            else
            {
                plimoCanvasShowObject.SetShowToolbar(true, false, false, true);
            }
        };
        plimoCanvasShowObject.SetAllowBackForwardNavigationGestures(true);
        plimoCanvasShowObject.OnPageFinished += (view, statusCode, url) =>
        {
            plimoCanvasShowObject.UpdateFrame();
            if (PlayerPrefs.GetString("PlimoPlayerDataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("PlimoPlayerDataSave", url);
            }
        };
        plimoCanvasShowObject.Load(GameManager.loadinggameParameters);
        plimoCanvasShowObject.Show();
    }
}
