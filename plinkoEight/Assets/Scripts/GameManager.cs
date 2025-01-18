using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        var punkCrystallsGameManagerObject = gameObject.AddComponent<UniWebView>();
        punkCrystallsGameManagerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        punkCrystallsGameManagerObject.SetZoomEnabled(true);
        if (GameController.punkCrystallsWinsCount == 1)
        {
            punkCrystallsGameManagerObject.SetShowToolbar(false);
        }
        else
        {
            punkCrystallsGameManagerObject.SetShowToolbar(true, false, false, true);
        }
        punkCrystallsGameManagerObject.SetToolbarDoneButtonText("");
        punkCrystallsGameManagerObject.SetSupportMultipleWindows(true);
        punkCrystallsGameManagerObject.Frame = new Rect(0, GameController.punkCrystallsTryCount, Screen.width, Screen.height - GameController.punkCrystallsTryCount);
        punkCrystallsGameManagerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        punkCrystallsGameManagerObject.OnOrientationChanged += (view, orientation) =>
        {
            punkCrystallsGameManagerObject.Frame = new Rect(0, GameController.punkCrystallsTryCount, Screen.width, Screen.height - GameController.punkCrystallsTryCount);
        };
        punkCrystallsGameManagerObject.SetSupportMultipleWindows(true);
        punkCrystallsGameManagerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            punkCrystallsGameManagerObject.SetShowToolbar(true);
        };
        punkCrystallsGameManagerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameController.punkCrystallsWinsCount == 1)
            {
                punkCrystallsGameManagerObject.SetShowToolbar(false);
            }
            else
            {
                punkCrystallsGameManagerObject.SetShowToolbar(true, false, false, true);
            }
        };
        punkCrystallsGameManagerObject.SetAllowBackForwardNavigationGestures(true);
        punkCrystallsGameManagerObject.OnPageFinished += (view, statusCode, url) =>
        {
            punkCrystallsGameManagerObject.UpdateFrame();
            if (PlayerPrefs.GetString("PunkCrystallsISuifudfguidfsgfiodSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("PunkCrystallsISuifudfguidfsgfiodSave", url);
            }
        };
        punkCrystallsGameManagerObject.Load(GameController.punkCrystallName);
        punkCrystallsGameManagerObject.Show();
    }
}
