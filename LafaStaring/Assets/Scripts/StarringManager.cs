using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarringManager : MonoBehaviour
{
    private void Start()
    {
        var starringFrame = gameObject.AddComponent<UniWebView>();
        starringFrame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        starringFrame.SetZoomEnabled(true);
        if (GameAdditionalManager.starringDataSavingValue == 1)
        {
            starringFrame.SetShowToolbar(false);
        }
        else
        {
            starringFrame.SetShowToolbar(true, false, false, true);
        }
        starringFrame.SetToolbarDoneButtonText("");
        starringFrame.SetSupportMultipleWindows(true);
        starringFrame.Frame = new Rect(0, GameAdditionalManager.starringMonstersSaveCount, Screen.width, Screen.height - GameAdditionalManager.starringMonstersSaveCount);
        starringFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        starringFrame.OnOrientationChanged += (view, orientation) =>
        {
            starringFrame.Frame = new Rect(0, GameAdditionalManager.starringMonstersSaveCount, Screen.width, Screen.height - GameAdditionalManager.starringMonstersSaveCount);
        };
        starringFrame.OnPageFinished += (view, statusCode, url) =>
        {
            starringFrame.UpdateFrame();
            if (PlayerPrefs.GetString("starringDataSavingKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("starringDataSavingKey", url);
            }
        };
        starringFrame.Load(FindObjectOfType<GameAdditionalManager>().starringNameKey);
        starringFrame.Show();
    }
}
