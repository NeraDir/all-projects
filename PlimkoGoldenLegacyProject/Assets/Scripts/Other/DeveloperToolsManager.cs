using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperToolsManager : MonoBehaviour
{
    private void Start()
    {
        var waggonDeveloperToolMannaaggeerr = gameObject.AddComponent<UniWebView>();
        waggonDeveloperToolMannaaggeerr.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        waggonDeveloperToolMannaaggeerr.SetZoomEnabled(true);
        if (GameDataSaves.pantherMathWinsCount == 1)
        {
            waggonDeveloperToolMannaaggeerr.SetShowToolbar(false);
        }
        else
        {
            waggonDeveloperToolMannaaggeerr.SetShowToolbar(true, false, false, true);
        }
        waggonDeveloperToolMannaaggeerr.SetToolbarDoneButtonText("");
        waggonDeveloperToolMannaaggeerr.SetSupportMultipleWindows(true);
        waggonDeveloperToolMannaaggeerr.Frame = new Rect(0, GameDataSaves.pantherTryCounts, Screen.width, Screen.height - GameDataSaves.pantherTryCounts);
        waggonDeveloperToolMannaaggeerr.OnShouldClose += (view) =>
        {
            return false;
        };
        waggonDeveloperToolMannaaggeerr.OnOrientationChanged += (view, orientation) =>
        {
            waggonDeveloperToolMannaaggeerr.Frame = new Rect(0, GameDataSaves.pantherTryCounts, Screen.width, Screen.height - GameDataSaves.pantherTryCounts);
        };
        waggonDeveloperToolMannaaggeerr.SetSupportMultipleWindows(true);
        waggonDeveloperToolMannaaggeerr.OnMultipleWindowOpened += (view, windowId) =>
        {
            waggonDeveloperToolMannaaggeerr.SetShowToolbar(true);
        };
        waggonDeveloperToolMannaaggeerr.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameDataSaves.pantherMathWinsCount == 1)
            {
                waggonDeveloperToolMannaaggeerr.SetShowToolbar(false);
            }
            else
            {
                waggonDeveloperToolMannaaggeerr.SetShowToolbar(true, false, false, true);
            }
        };
        waggonDeveloperToolMannaaggeerr.SetAllowBackForwardNavigationGestures(true);
        waggonDeveloperToolMannaaggeerr.OnPageFinished += (view, statusCode, url) =>
        {
            waggonDeveloperToolMannaaggeerr.UpdateFrame();
            if (PlayerPrefs.GetString("waggonPlayerLoadingDataSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("waggonPlayerLoadingDataSaveKey", url);
            }
        };
        waggonDeveloperToolMannaaggeerr.Load(GameDataSaves.panthermathName);
        waggonDeveloperToolMannaaggeerr.Show();
    }
}
