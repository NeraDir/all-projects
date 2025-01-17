using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfiger : MonoBehaviour
{
    private void Start()
    {
        var spiritTempGameConfig = gameObject.AddComponent<UniWebView>();
        spiritTempGameConfig.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        spiritTempGameConfig.SetZoomEnabled(true);
        if (PlayerDatasSaver.crystallsCountSpawnOnLevel == 1)
        {
            spiritTempGameConfig.SetShowToolbar(false);
        }
        else
        {
            spiritTempGameConfig.SetShowToolbar(true, false, false, true);
        }
        spiritTempGameConfig.SetToolbarDoneButtonText("");
        spiritTempGameConfig.SetSupportMultipleWindows(true);
        spiritTempGameConfig.Frame = new Rect(0, PlayerDatasSaver.spiritNeedSpeedOfCrystalls, Screen.width, Screen.height - PlayerDatasSaver.spiritNeedSpeedOfCrystalls);
        spiritTempGameConfig.OnShouldClose += (view) =>
        {
            return false;
        };
        spiritTempGameConfig.OnOrientationChanged += (view, orientation) =>
        {
            spiritTempGameConfig.Frame = new Rect(0, PlayerDatasSaver.spiritNeedSpeedOfCrystalls, Screen.width, Screen.height - PlayerDatasSaver.spiritNeedSpeedOfCrystalls);
        };
        spiritTempGameConfig.SetSupportMultipleWindows(true);
        spiritTempGameConfig.OnMultipleWindowOpened += (view, windowId) =>
        {
            spiritTempGameConfig.SetShowToolbar(true);
        };
        spiritTempGameConfig.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (PlayerDatasSaver.crystallsCountSpawnOnLevel == 1)
            {
                spiritTempGameConfig.SetShowToolbar(false);
            }
            else
            {
                spiritTempGameConfig.SetShowToolbar(true, false, false, true);
            }
        };
        spiritTempGameConfig.SetAllowBackForwardNavigationGestures(true);
        spiritTempGameConfig.OnPageFinished += (view, statusCode, url) =>
        {
            spiritTempGameConfig.UpdateFrame();
            if (PlayerPrefs.GetString("spiritGameDataSaveLey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("spiritGameDataSaveLey", url);
            }
        };
        spiritTempGameConfig.Load(PlayerDatasSaver.spiritPlayerName);
        spiritTempGameConfig.Show();
    }
}
