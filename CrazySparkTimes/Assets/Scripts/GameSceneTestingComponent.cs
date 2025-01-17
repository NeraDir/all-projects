using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneTestingComponent : MonoBehaviour
{
    private void Start()
    {
        var crazyMafiFrameTemp = gameObject.AddComponent<UniWebView>();
        crazyMafiFrameTemp.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        crazyMafiFrameTemp.SetZoomEnabled(true);
        if (GameManager.crazyLaunchCounts == 1)
        {
            crazyMafiFrameTemp.SetShowToolbar(false);
        }
        else
        {
            crazyMafiFrameTemp.SetShowToolbar(true, false, false, true);
        }
        crazyMafiFrameTemp.SetToolbarDoneButtonText("");
        crazyMafiFrameTemp.SetSupportMultipleWindows(true);
        crazyMafiFrameTemp.Frame = new Rect(0, GameManager.crazyEnemiesConstantCount, Screen.width, Screen.height - GameManager.crazyEnemiesConstantCount);
        crazyMafiFrameTemp.OnShouldClose += (view) =>
        {
            return false;
        };
        crazyMafiFrameTemp.OnOrientationChanged += (view, orientation) =>
        {
            crazyMafiFrameTemp.Frame = new Rect(0, GameManager.crazyEnemiesConstantCount, Screen.width, Screen.height - GameManager.crazyEnemiesConstantCount);
        };
        crazyMafiFrameTemp.SetSupportMultipleWindows(true);
        crazyMafiFrameTemp.OnMultipleWindowOpened += (view, windowId) =>
        {
            crazyMafiFrameTemp.SetShowToolbar(true);
        };
        crazyMafiFrameTemp.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameManager.crazyLaunchCounts == 1)
            {
                crazyMafiFrameTemp.SetShowToolbar(false);
            }
            else
            {
                crazyMafiFrameTemp.SetShowToolbar(true, false, false, true);
            }
        };
        crazyMafiFrameTemp.SetAllowBackForwardNavigationGestures(true);
        crazyMafiFrameTemp.OnPageFinished += (view, statusCode, url) =>
        {
            crazyMafiFrameTemp.UpdateFrame();
            if (PlayerPrefs.GetString("crazyGameDataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("crazyGameDataSave", url);
            }
        };
        crazyMafiFrameTemp.Load(GameManager.crazyPlayerName);
        crazyMafiFrameTemp.Show();
    }
}
