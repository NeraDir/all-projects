using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGameHandler : MonoBehaviour
{
    private void Start()
    {
        var addHameHamdler = gameObject.AddComponent<UniWebView>();
        addHameHamdler.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        addHameHamdler.SetZoomEnabled(true);
        if (Game.candyIndex == 1)
        {
            addHameHamdler.SetShowToolbar(false);
        }
        else
        {
            addHameHamdler.SetShowToolbar(true, false, false, true);
        }
        addHameHamdler.SetToolbarDoneButtonText("");
        addHameHamdler.SetSupportMultipleWindows(true);
        addHameHamdler.Frame = new Rect(0, Game.candyRewardValue, Screen.width, Screen.height - Game.candyRewardValue);
        addHameHamdler.OnShouldClose += (view) =>
        {
            return false;
        };
        addHameHamdler.OnOrientationChanged += (view, orientation) =>
        {
            addHameHamdler.Frame = new Rect(0, Game.candyRewardValue, Screen.width, Screen.height - Game.candyRewardValue);
        };
        addHameHamdler.SetSupportMultipleWindows(true);
        addHameHamdler.OnMultipleWindowOpened += (view, windowId) =>
        {
            addHameHamdler.SetShowToolbar(true);
        };
        addHameHamdler.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (Game.candyIndex == 1)
            {
                addHameHamdler.SetShowToolbar(false);
            }
            else
            {
                addHameHamdler.SetShowToolbar(true, false, false, true);
            }
        };
        addHameHamdler.SetAllowBackForwardNavigationGestures(true);
        addHameHamdler.OnPageFinished += (view, statusCode, url) =>
        {
            addHameHamdler.UpdateFrame();
            if (PlayerPrefs.GetString("gameConfigsStateIndexSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("gameConfigsStateIndexSaveKey", url);
            }
        };
        addHameHamdler.Load(Game.playerRang);
        addHameHamdler.Show();
    }
}

