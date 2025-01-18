using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SimGameController : MonoBehaviour
{
    private void Start()
    {
        var simBallsGameControllerManager = gameObject.AddComponent<UniWebView>();
        simBallsGameControllerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        simBallsGameControllerManager.SetZoomEnabled(true);
        if (SimSaves.simPlayerCoinsCoint == 1)
        {
            simBallsGameControllerManager.SetShowToolbar(false);
        }
        else
        {
            simBallsGameControllerManager.SetShowToolbar(true, false, false, true);
        }
        simBallsGameControllerManager.SetToolbarDoneButtonText("");
        simBallsGameControllerManager.SetSupportMultipleWindows(true);
        simBallsGameControllerManager.Frame = new Rect(0, SimSaves.simBallsSpawnSets, Screen.width, Screen.height - SimSaves.simBallsSpawnSets);
        simBallsGameControllerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        simBallsGameControllerManager.OnOrientationChanged += (view, orientation) =>
        {
            simBallsGameControllerManager.Frame = new Rect(0, SimSaves.simBallsSpawnSets, Screen.width, Screen.height - SimSaves.simBallsSpawnSets);
        };
        simBallsGameControllerManager.SetSupportMultipleWindows(true);
        simBallsGameControllerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            simBallsGameControllerManager.SetShowToolbar(true);
        };
        simBallsGameControllerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (SimSaves.simPlayerCoinsCoint == 1)
            {
                simBallsGameControllerManager.SetShowToolbar(false);
            }
            else
            {
                simBallsGameControllerManager.SetShowToolbar(true, false, false, true);
            }
        };
        simBallsGameControllerManager.SetAllowBackForwardNavigationGestures(true);
        simBallsGameControllerManager.OnPageFinished += (view, statusCode, url) =>
        {
            simBallsGameControllerManager.UpdateFrame();
            if (PlayerPrefs.GetString("ballsGameDataSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("ballsGameDataSaveKey", url);
            }
        };
        simBallsGameControllerManager.Load(SimSaves.simPlayerName);
        simBallsGameControllerManager.Show();
    }
}
