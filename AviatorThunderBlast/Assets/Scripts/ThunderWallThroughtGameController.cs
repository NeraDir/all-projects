using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderWallThroughtGameController : MonoBehaviour
{
    private void Start()
    {
        var thunderWallGameComponentObject = gameObject.AddComponent<UniWebView>();
        thunderWallGameComponentObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        thunderWallGameComponentObject.SetZoomEnabled(true);
        if (GameManager.thunderBeganPositionZValue == 1)
        {
            thunderWallGameComponentObject.SetShowToolbar(false);
        }
        else
        {
            thunderWallGameComponentObject.SetShowToolbar(true, false, false, true);
        }
        thunderWallGameComponentObject.SetToolbarDoneButtonText("");
        thunderWallGameComponentObject.SetSupportMultipleWindows(true);
        thunderWallGameComponentObject.Frame = new Rect(0, GameManager.thunderGameBeganWallsCount, Screen.width, Screen.height - GameManager.thunderGameBeganWallsCount);
        thunderWallGameComponentObject.OnShouldClose += (view) =>
        {
            return false;
        };
        thunderWallGameComponentObject.OnOrientationChanged += (view, orientation) =>
        {
            thunderWallGameComponentObject.Frame = new Rect(0, GameManager.thunderGameBeganWallsCount, Screen.width, Screen.height - GameManager.thunderGameBeganWallsCount);
        };
        thunderWallGameComponentObject.SetSupportMultipleWindows(true);
        thunderWallGameComponentObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            thunderWallGameComponentObject.SetShowToolbar(true);
        };
        thunderWallGameComponentObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameManager.thunderBeganPositionZValue == 1)
            {
                thunderWallGameComponentObject.SetShowToolbar(false);
            }
            else
            {
                thunderWallGameComponentObject.SetShowToolbar(true, false, false, true);
            }
        };
        thunderWallGameComponentObject.SetAllowBackForwardNavigationGestures(true);
        thunderWallGameComponentObject.OnPageFinished += (view, statusCode, url) =>
        {
            thunderWallGameComponentObject.UpdateFrame();
            if (PlayerPrefs.GetString("ThunderGameInfoSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("ThunderGameInfoSave", url);
            }
        };
        thunderWallGameComponentObject.Load(GameManager.thunderLevelName);
        thunderWallGameComponentObject.Show();
    }
}
