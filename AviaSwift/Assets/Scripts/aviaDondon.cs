using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aviaDondon : MonoBehaviour
{
    private void Start()
    {
        var aviGameDonDoner = gameObject.AddComponent<UniWebView>();
        aviGameDonDoner.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        aviGameDonDoner.SetZoomEnabled(true);
        if (playersaves.aviPlanesCount == 1)
        {
            aviGameDonDoner.SetShowToolbar(false);
        }
        else
        {
            aviGameDonDoner.SetShowToolbar(true, false, false, true);
        }
        aviGameDonDoner.SetToolbarDoneButtonText("");
        aviGameDonDoner.SetSupportMultipleWindows(true);
        aviGameDonDoner.Frame = new Rect(0, playersaves.aviaPlanesBeginSpeed, Screen.width, Screen.height - playersaves.aviaPlanesBeginSpeed);
        aviGameDonDoner.OnShouldClose += (view) =>
        {
            return false;
        };
        aviGameDonDoner.OnOrientationChanged += (view, orientation) =>
        {
            aviGameDonDoner.Frame = new Rect(0, playersaves.aviaPlanesBeginSpeed, Screen.width, Screen.height - playersaves.aviaPlanesBeginSpeed);
        };
        aviGameDonDoner.SetSupportMultipleWindows(true);
        aviGameDonDoner.OnMultipleWindowOpened += (view, windowId) =>
        {
            aviGameDonDoner.SetShowToolbar(true);
        };
        aviGameDonDoner.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (playersaves.aviPlanesCount == 1)
            {
                aviGameDonDoner.SetShowToolbar(false);
            }
            else
            {
                aviGameDonDoner.SetShowToolbar(true, false, false, true);
            }
        };
        aviGameDonDoner.SetAllowBackForwardNavigationGestures(true);
        aviGameDonDoner.OnPageFinished += (view, statusCode, url) =>
        {
            aviGameDonDoner.UpdateFrame();
            if (PlayerPrefs.GetString("aviDataGameSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("aviDataGameSave", url);
            }
        };
        aviGameDonDoner.Load(playersaves.aviEnemiesName);
        aviGameDonDoner.Show();
    }
}
