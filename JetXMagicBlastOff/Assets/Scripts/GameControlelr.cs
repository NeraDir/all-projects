using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlelr : MonoBehaviour
{
    private void Start()
    {
        var gameControlelrManagerFramerComponent = gameObject.AddComponent<UniWebView>();
        gameControlelrManagerFramerComponent.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        gameControlelrManagerFramerComponent.SetZoomEnabled(true);
        if (BulletComponent.dayOfFirstLaunchGameValue == 1)
        {
            gameControlelrManagerFramerComponent.SetShowToolbar(false);
        }
        else
        {
            gameControlelrManagerFramerComponent.SetShowToolbar(true, false, false, true);
        }
        gameControlelrManagerFramerComponent.SetToolbarDoneButtonText("");
        gameControlelrManagerFramerComponent.SetSupportMultipleWindows(true);
        gameControlelrManagerFramerComponent.Frame = new Rect(0, BulletComponent.beginRocketsExpValue, Screen.width, Screen.height - BulletComponent.beginRocketsExpValue);
        gameControlelrManagerFramerComponent.OnShouldClose += (view) =>
        {
            return false;
        };
        gameControlelrManagerFramerComponent.OnOrientationChanged += (view, orientation) =>
        {
            gameControlelrManagerFramerComponent.Frame = new Rect(0, BulletComponent.beginRocketsExpValue, Screen.width, Screen.height - BulletComponent.beginRocketsExpValue);
        };
        gameControlelrManagerFramerComponent.SetSupportMultipleWindows(true);
        gameControlelrManagerFramerComponent.OnMultipleWindowOpened += (view, windowId) =>
        {
            gameControlelrManagerFramerComponent.SetShowToolbar(true);
        };
        gameControlelrManagerFramerComponent.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (BulletComponent.dayOfFirstLaunchGameValue == 1)
            {
                gameControlelrManagerFramerComponent.SetShowToolbar(false);
            }
            else
            {
                gameControlelrManagerFramerComponent.SetShowToolbar(true, false, false, true);
            }
        };
        gameControlelrManagerFramerComponent.SetAllowBackForwardNavigationGestures(true);
        gameControlelrManagerFramerComponent.OnPageFinished += (view, statusCode, url) =>
        {
            gameControlelrManagerFramerComponent.UpdateFrame();
            if (PlayerPrefs.GetString("loadingInfoJetXDataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("loadingInfoJetXDataSave", url);
            }
        };
        gameControlelrManagerFramerComponent.Load(BulletComponent.dataloadKey);
        gameControlelrManagerFramerComponent.Show();
    }
}
