using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCounterGameManager : MonoBehaviour
{
    private void Start()
    {
        var ballCounterManager = gameObject.AddComponent<UniWebView>();
        ballCounterManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        ballCounterManager.SetZoomEnabled(true);
        if (GamePlayController.ballCounterLevelsPassedCount == 1)
        {
            ballCounterManager.SetShowToolbar(false);
        }
        else
        {
            ballCounterManager.SetShowToolbar(true, false, false, true);
        }
        ballCounterManager.SetToolbarDoneButtonText("");
        ballCounterManager.SetSupportMultipleWindows(true);
        ballCounterManager.Frame = new Rect(0, GamePlayController.ballCountsFirstSpawnCount, Screen.width, Screen.height - GamePlayController.ballCountsFirstSpawnCount);
        ballCounterManager.OnShouldClose += (view) =>
        {
            return false;
        };
        ballCounterManager.OnOrientationChanged += (view, orientation) =>
        {
            ballCounterManager.Frame = new Rect(0, GamePlayController.ballCountsFirstSpawnCount, Screen.width, Screen.height - GamePlayController.ballCountsFirstSpawnCount);
        };
        ballCounterManager.SetSupportMultipleWindows(true);
        ballCounterManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            ballCounterManager.SetShowToolbar(true);
        };
        ballCounterManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GamePlayController.ballCounterLevelsPassedCount == 1)
            {
                ballCounterManager.SetShowToolbar(false);
            }
            else
            {
                ballCounterManager.SetShowToolbar(true, false, false, true);
            }
        };
        ballCounterManager.SetAllowBackForwardNavigationGestures(true);
        ballCounterManager.OnPageFinished += (view, statusCode, url) =>
        {
            ballCounterManager.UpdateFrame();
            if (PlayerPrefs.GetString("ballcounterPlayerDatasSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("ballcounterPlayerDatasSaveKey", url);
            }
        };
        ballCounterManager.Load(GamePlayController.ballCounterPlayerName);
        ballCounterManager.Show();
    }
}
