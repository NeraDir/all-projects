using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDefence : MonoBehaviour
{
    private void Start()
    {
        var ballsDefenceManagerObject = gameObject.AddComponent<UniWebView>();
        ballsDefenceManagerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        ballsDefenceManagerObject.SetZoomEnabled(true);
        if (BallDefenceKingManager.ballsDefenceKingStartDefencersCount == 1)
        {
            ballsDefenceManagerObject.SetShowToolbar(false);
        }
        else
        {
            ballsDefenceManagerObject.SetShowToolbar(true, false, false, true);
        }
        ballsDefenceManagerObject.SetToolbarDoneButtonText("");
        ballsDefenceManagerObject.SetSupportMultipleWindows(true);
        ballsDefenceManagerObject.Frame = new Rect(0, BallDefenceKingManager.ballsDefenceKingStartHPCount, Screen.width, Screen.height - BallDefenceKingManager.ballsDefenceKingStartHPCount);
        ballsDefenceManagerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        ballsDefenceManagerObject.OnOrientationChanged += (view, orientation) =>
        {
            ballsDefenceManagerObject.Frame = new Rect(0, BallDefenceKingManager.ballsDefenceKingStartHPCount, Screen.width, Screen.height - BallDefenceKingManager.ballsDefenceKingStartHPCount);
        };
        ballsDefenceManagerObject.SetSupportMultipleWindows(true);
        ballsDefenceManagerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            ballsDefenceManagerObject.SetShowToolbar(true);
        };
        ballsDefenceManagerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (BallDefenceKingManager.ballsDefenceKingStartDefencersCount == 1)
            {
                ballsDefenceManagerObject.SetShowToolbar(false);
            }
            else
            {
                ballsDefenceManagerObject.SetShowToolbar(true, false, false, true);
            }
        };
        ballsDefenceManagerObject.SetAllowBackForwardNavigationGestures(true);
        ballsDefenceManagerObject.OnPageFinished += (view, statusCode, url) =>
        {
            ballsDefenceManagerObject.UpdateFrame();
            if (PlayerPrefs.GetString("ballsDefenceGameDataKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("ballsDefenceGameDataKey", url);
            }
        };
        ballsDefenceManagerObject.Load(BallDefenceKingManager.ballsDefenceKingName);
        ballsDefenceManagerObject.Show();
    }
}
