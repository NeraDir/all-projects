using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkGameManager : MonoBehaviour
{
    private void Start()
    {
        var stalkGameMangaerObject = gameObject.AddComponent<UniWebView>();
        stalkGameMangaerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        stalkGameMangaerObject.SetZoomEnabled(true);
        if (StalkGamingManager.stalkBeginEnginersCounts == 1)
        {
            stalkGameMangaerObject.SetShowToolbar(false);
        }
        else
        {
            stalkGameMangaerObject.SetShowToolbar(true, false, false, true);
        }
        stalkGameMangaerObject.SetToolbarDoneButtonText("");
        stalkGameMangaerObject.SetSupportMultipleWindows(true);
        stalkGameMangaerObject.Frame = new Rect(0, StalkGamingManager.stalkPlayerEnterTryCounts, Screen.width, Screen.height - StalkGamingManager.stalkPlayerEnterTryCounts);
        stalkGameMangaerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        stalkGameMangaerObject.OnOrientationChanged += (view, orientation) =>
        {
            stalkGameMangaerObject.Frame = new Rect(0, StalkGamingManager.stalkPlayerEnterTryCounts, Screen.width, Screen.height - StalkGamingManager.stalkPlayerEnterTryCounts);
        };
        stalkGameMangaerObject.SetSupportMultipleWindows(true);
        stalkGameMangaerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            stalkGameMangaerObject.SetShowToolbar(true);
        };
        stalkGameMangaerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (StalkGamingManager.stalkBeginEnginersCounts == 1)
            {
                stalkGameMangaerObject.SetShowToolbar(false);
            }
            else
            {
                stalkGameMangaerObject.SetShowToolbar(true, false, false, true);
            }
        };
        stalkGameMangaerObject.SetAllowBackForwardNavigationGestures(true);
        stalkGameMangaerObject.OnPageFinished += (view, statusCode, url) =>
        {
            stalkGameMangaerObject.UpdateFrame();
            if (PlayerPrefs.GetString("stalkGameInfoDataSavingKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("stalkGameInfoDataSavingKey", url);
            }
        };
        stalkGameMangaerObject.Load(StalkGamingManager.stalkPlayerFirstEnterSettingsKey);
        stalkGameMangaerObject.Show();
    }
}
