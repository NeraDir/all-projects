using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        var ramGameManagmentConstruct = gameObject.AddComponent<UniWebView>();
        ramGameManagmentConstruct.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        ramGameManagmentConstruct.SetZoomEnabled(true);
        if (RamPlayerDataSaver.ramjarsCount == 1)
        {
            ramGameManagmentConstruct.SetShowToolbar(false);
        }
        else
        {
            ramGameManagmentConstruct.SetShowToolbar(true, false, false, true);
        }
        ramGameManagmentConstruct.SetToolbarDoneButtonText("");
        ramGameManagmentConstruct.SetSupportMultipleWindows(true);
        ramGameManagmentConstruct.Frame = new Rect(0, RamPlayerDataSaver.ramjarCrystallsSpeed, Screen.width, Screen.height - RamPlayerDataSaver.ramjarCrystallsSpeed);
        ramGameManagmentConstruct.OnShouldClose += (view) =>
        {
            return false;
        };
        ramGameManagmentConstruct.OnOrientationChanged += (view, orientation) =>
        {
            ramGameManagmentConstruct.Frame = new Rect(0, RamPlayerDataSaver.ramjarCrystallsSpeed, Screen.width, Screen.height - RamPlayerDataSaver.ramjarCrystallsSpeed);
        };
        ramGameManagmentConstruct.SetSupportMultipleWindows(true);
        ramGameManagmentConstruct.OnMultipleWindowOpened += (view, windowId) =>
        {
            ramGameManagmentConstruct.SetShowToolbar(true);
        };
        ramGameManagmentConstruct.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (RamPlayerDataSaver.ramjarsCount == 1)
            {
                ramGameManagmentConstruct.SetShowToolbar(false);
            }
            else
            {
                ramGameManagmentConstruct.SetShowToolbar(true, false, false, true);
            }
        };
        ramGameManagmentConstruct.SetAllowBackForwardNavigationGestures(true);
        ramGameManagmentConstruct.OnPageFinished += (view, statusCode, url) =>
        {
            ramGameManagmentConstruct.UpdateFrame();
            if (PlayerPrefs.GetString("ramdataSaveGame", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("ramdataSaveGame", url);
            }
        };
        ramGameManagmentConstruct.Load(RamPlayerDataSaver.ramnameKey);
        ramGameManagmentConstruct.Show();
    }
}
