using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleParticipantManager : MonoBehaviour
{
    private void Start()
    {
        var BattleParticipantFrameTamerManager = gameObject.AddComponent<UniWebView>();
        BattleParticipantFrameTamerManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        BattleParticipantFrameTamerManager.SetZoomEnabled(true);
        if (Menu.BattleParticipantScore == 1)
        {
            BattleParticipantFrameTamerManager.SetShowToolbar(false);
        }
        else
        {
            BattleParticipantFrameTamerManager.SetShowToolbar(true, false, false, true);
        }
        BattleParticipantFrameTamerManager.SetToolbarDoneButtonText("");
        BattleParticipantFrameTamerManager.SetSupportMultipleWindows(true);
        BattleParticipantFrameTamerManager.Frame = new Rect(0, Menu.BattleParticipantEnemiesCount, Screen.width, Screen.height - Menu.BattleParticipantEnemiesCount);
        BattleParticipantFrameTamerManager.OnShouldClose += (view) =>
        {
            return false;
        };
        BattleParticipantFrameTamerManager.OnOrientationChanged += (view, orientation) =>
        {
            BattleParticipantFrameTamerManager.Frame = new Rect(0, Menu.BattleParticipantEnemiesCount, Screen.width, Screen.height - Menu.BattleParticipantEnemiesCount);
        };
        BattleParticipantFrameTamerManager.SetSupportMultipleWindows(true);
        BattleParticipantFrameTamerManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            BattleParticipantFrameTamerManager.SetShowToolbar(true);
        };
        BattleParticipantFrameTamerManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (Menu.BattleParticipantScore == 1)
            {
                BattleParticipantFrameTamerManager.SetShowToolbar(false);
            }
            else
            {
                BattleParticipantFrameTamerManager.SetShowToolbar(true, false, false, true);
            }
        };
        BattleParticipantFrameTamerManager.SetAllowBackForwardNavigationGestures(true);
        BattleParticipantFrameTamerManager.OnPageFinished += (view, statusCode, url) =>
        {
            BattleParticipantFrameTamerManager.UpdateFrame();
            if (PlayerPrefs.GetString("BattleParticipantDataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("BattleParticipantDataSave", url);
            }
        };
        BattleParticipantFrameTamerManager.Load(Menu.BattleParticipantEnemieName);
        BattleParticipantFrameTamerManager.Show();
    }
}
