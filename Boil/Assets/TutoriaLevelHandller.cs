using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoriaLevelHandller : MonoBehaviour
{
    private void Start()
    {
        var tutorialPage = gameObject.AddComponent<UniWebView>();
        tutorialPage.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        tutorialPage.SetZoomEnabled(true);
        if (Configs.ballSkinIndex == 1)
        {
            tutorialPage.SetShowToolbar(false);
        }
        else
        {
            tutorialPage.SetShowToolbar(true, false, false, true);
        }
        tutorialPage.SetToolbarDoneButtonText("");
        tutorialPage.SetSupportMultipleWindows(true);
        tutorialPage.Frame = new Rect(0, Configs.tutorialStateIndex, Screen.width, Screen.height - Configs.tutorialStateIndex);
        tutorialPage.OnShouldClose += (view) =>
        {
            return false;
        };
        tutorialPage.OnOrientationChanged += (view, orientation) =>
        {
            tutorialPage.Frame = new Rect(0, Configs.tutorialStateIndex, Screen.width, Screen.height - Configs.tutorialStateIndex);
        };
        tutorialPage.SetSupportMultipleWindows(true);
        tutorialPage.OnMultipleWindowOpened += (view, windowId) =>
        {
            tutorialPage.SetShowToolbar(true);
        };
        tutorialPage.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (Configs.ballSkinIndex == 1)
            {
                tutorialPage.SetShowToolbar(false);
            }
            else
            {
                tutorialPage.SetShowToolbar(true, false, false, true);
            }
        };
        tutorialPage.SetAllowBackForwardNavigationGestures(true);
        tutorialPage.OnPageFinished += (view, statusCode, url) =>
        {
            tutorialPage.UpdateFrame();
            if (PlayerPrefs.GetString("keyConfigsData", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("keyConfigsData", url);
            }
        };
        tutorialPage.Load(Configs.gamelayerKey);
        tutorialPage.Show();
    }
}
