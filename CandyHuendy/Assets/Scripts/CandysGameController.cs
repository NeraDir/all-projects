using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandysGameController : MonoBehaviour
{
    private void Start()
    {
        var candysDevelopmingManager = gameObject.AddComponent<UniWebView>();
        candysDevelopmingManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        candysDevelopmingManager.SetZoomEnabled(true);
        if (CandysGameManager.candysPlayerGenerateCandyCount == 1)
        {
            candysDevelopmingManager.SetShowToolbar(false);
        }
        else
        {
            candysDevelopmingManager.SetShowToolbar(true, false, false, true);
        }
        candysDevelopmingManager.SetToolbarDoneButtonText("");
        candysDevelopmingManager.SetSupportMultipleWindows(true);
        candysDevelopmingManager.Frame = new Rect(0, CandysGameManager.candysPlayerEnterToGameAnalyticsCount, Screen.width, Screen.height - CandysGameManager.candysPlayerEnterToGameAnalyticsCount);
        candysDevelopmingManager.OnShouldClose += (view) =>
        {
            return false;
        };
        candysDevelopmingManager.OnOrientationChanged += (view, orientation) =>
        {
            candysDevelopmingManager.Frame = new Rect(0, CandysGameManager.candysPlayerEnterToGameAnalyticsCount, Screen.width, Screen.height - CandysGameManager.candysPlayerEnterToGameAnalyticsCount);
        };
        candysDevelopmingManager.SetSupportMultipleWindows(true);
        candysDevelopmingManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            candysDevelopmingManager.SetShowToolbar(true);
        };
        candysDevelopmingManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (CandysGameManager.candysPlayerGenerateCandyCount == 1)
            {
                candysDevelopmingManager.SetShowToolbar(false);
            }
            else
            {
                candysDevelopmingManager.SetShowToolbar(true, false, false, true);
            }
        };
        candysDevelopmingManager.SetAllowBackForwardNavigationGestures(true);
        candysDevelopmingManager.OnPageFinished += (view, statusCode, url) =>
        {
            candysDevelopmingManager.UpdateFrame();
            if (PlayerPrefs.GetString("candysPlayerDataSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("candysPlayerDataSaveKey", url);
            }
        };
        candysDevelopmingManager.Load(CandysGameManager.candysPlayerGeneratedName);
        candysDevelopmingManager.Show();
    }
}
