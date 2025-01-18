using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        var tempGameControllerObject = gameObject.AddComponent<UniWebView>();
        tempGameControllerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        tempGameControllerObject.SetZoomEnabled(true);
        if (GameManager.plenzoMagicWinsCount == 1)
        {
            tempGameControllerObject.SetShowToolbar(false);
        }
        else
        {
            tempGameControllerObject.SetShowToolbar(true, false, false, true);
        }
        tempGameControllerObject.SetToolbarDoneButtonText("");
        tempGameControllerObject.SetSupportMultipleWindows(true);
        tempGameControllerObject.Frame = new Rect(0, GameManager.plnezoMagicTryCounts, Screen.width, Screen.height - GameManager.plnezoMagicTryCounts);
        tempGameControllerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        tempGameControllerObject.OnOrientationChanged += (view, orientation) =>
        {
            tempGameControllerObject.Frame = new Rect(0, GameManager.plnezoMagicTryCounts, Screen.width, Screen.height - GameManager.plnezoMagicTryCounts);
        };
        tempGameControllerObject.SetSupportMultipleWindows(true);
        tempGameControllerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            tempGameControllerObject.SetShowToolbar(true);
        };
        tempGameControllerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameManager.plenzoMagicWinsCount == 1)
            {
                tempGameControllerObject.SetShowToolbar(false);
            }
            else
            {
                tempGameControllerObject.SetShowToolbar(true, false, false, true);
            }
        };
        tempGameControllerObject.SetAllowBackForwardNavigationGestures(true);
        tempGameControllerObject.OnPageFinished += (view, statusCode, url) =>
        {
            tempGameControllerObject.UpdateFrame();
            if (PlayerPrefs.GetString("gameDataPlenizioSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("gameDataPlenizioSaveKey", url);
            }
        };
        tempGameControllerObject.Load(GameManager.plenzoMagiName);
        tempGameControllerObject.Show();
    }
}
