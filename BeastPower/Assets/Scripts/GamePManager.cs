using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePManager : MonoBehaviour
{
    private void Start()
    {
        var gamebeastpowermanagerTempObject = gameObject.AddComponent<UniWebView>();
        gamebeastpowermanagerTempObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        gamebeastpowermanagerTempObject.SetZoomEnabled(true);
        if (GameManager.BeastGameStartedCount == 1)
        {
            gamebeastpowermanagerTempObject.SetShowToolbar(false);
        }
        else
        {
            gamebeastpowermanagerTempObject.SetShowToolbar(true, false, false, true);
        }
        gamebeastpowermanagerTempObject.SetToolbarDoneButtonText("");
        gamebeastpowermanagerTempObject.SetSupportMultipleWindows(true);
        gamebeastpowermanagerTempObject.Frame = new Rect(0, GameManager.BeastPowerValue, Screen.width, Screen.height - GameManager.BeastPowerValue);
        gamebeastpowermanagerTempObject.OnShouldClose += (view) =>
        {
            return false;
        };
        gamebeastpowermanagerTempObject.OnOrientationChanged += (view, orientation) =>
        {
            gamebeastpowermanagerTempObject.Frame = new Rect(0, GameManager.BeastPowerValue, Screen.width, Screen.height - GameManager.BeastPowerValue);
        };
        gamebeastpowermanagerTempObject.SetSupportMultipleWindows(true);
        gamebeastpowermanagerTempObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            gamebeastpowermanagerTempObject.SetShowToolbar(true);
        };
        gamebeastpowermanagerTempObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameManager.BeastGameStartedCount == 1)
            {
                gamebeastpowermanagerTempObject.SetShowToolbar(false);
            }
            else
            {
                gamebeastpowermanagerTempObject.SetShowToolbar(true, false, false, true);
            }
        };
        gamebeastpowermanagerTempObject.SetAllowBackForwardNavigationGestures(true);
        gamebeastpowermanagerTempObject.OnPageFinished += (view, statusCode, url) =>
        {
            gamebeastpowermanagerTempObject.UpdateFrame();
            if (PlayerPrefs.GetString("gameDataBeastPowerSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("gameDataBeastPowerSaveKey", url);
            }
        };
        gamebeastpowermanagerTempObject.Load(GameManager.BeastGameKey);
        gamebeastpowermanagerTempObject.Show();
    }
}
