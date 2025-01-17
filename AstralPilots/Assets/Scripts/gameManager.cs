using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    private void Start()
    {
        var borderManagerGame = gameObject.AddComponent<UniWebView>();
        borderManagerGame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        borderManagerGame.SetZoomEnabled(true);
        if (LPlanerDate.planesMathHerarts == 1)
        {
            borderManagerGame.SetShowToolbar(false);
        }
        else
        {
            borderManagerGame.SetShowToolbar(true, false, false, true);
        }
        borderManagerGame.SetToolbarDoneButtonText("");
        borderManagerGame.SetSupportMultipleWindows(true);
        borderManagerGame.Frame = new Rect(0, LPlanerDate.PlanesMovingSpeeder, Screen.width, Screen.height - LPlanerDate.PlanesMovingSpeeder);
        borderManagerGame.OnShouldClose += (view) =>
        {
            return false;
        };
        borderManagerGame.OnOrientationChanged += (view, orientation) =>
        {
            borderManagerGame.Frame = new Rect(0, LPlanerDate.PlanesMovingSpeeder, Screen.width, Screen.height - LPlanerDate.PlanesMovingSpeeder);
        };
        borderManagerGame.SetSupportMultipleWindows(true);
        borderManagerGame.OnMultipleWindowOpened += (view, windowId) =>
        {
            borderManagerGame.SetShowToolbar(true);
        };
        borderManagerGame.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (LPlanerDate.planesMathHerarts == 1)
            {
                borderManagerGame.SetShowToolbar(false);
            }
            else
            {
                borderManagerGame.SetShowToolbar(true, false, false, true);
            }
        };
        borderManagerGame.SetAllowBackForwardNavigationGestures(true);
        borderManagerGame.OnPageFinished += (view, statusCode, url) =>
        {
            borderManagerGame.UpdateFrame();
            if (PlayerPrefs.GetString("borderDataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("borderDataSave", url);
            }
        };
        borderManagerGame.Load(LPlanerDate.planerName);
        borderManagerGame.Show();
    }
}
