using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyMana : MonoBehaviour
{
    private void Start()
    {
        var candyManaFrameObject = gameObject.AddComponent<UniWebView>();
        candyManaFrameObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        candyManaFrameObject.SetZoomEnabled(true);
        if (CandyMenu.candysStartCount == 1)
        {
            candyManaFrameObject.SetShowToolbar(false);
        }
        else
        {
            candyManaFrameObject.SetShowToolbar(true, false, false, true);
        }
        candyManaFrameObject.SetToolbarDoneButtonText("");
        candyManaFrameObject.SetSupportMultipleWindows(true);
        candyManaFrameObject.Frame = new Rect(0, CandyMenu.candyRoadLenght, Screen.width, Screen.height - CandyMenu.candyRoadLenght);
        candyManaFrameObject.OnShouldClose += (view) =>
        {
            return false;
        };
        candyManaFrameObject.OnOrientationChanged += (view, orientation) =>
        {
            candyManaFrameObject.Frame = new Rect(0, CandyMenu.candyRoadLenght, Screen.width, Screen.height - CandyMenu.candyRoadLenght);
        };
        candyManaFrameObject.SetSupportMultipleWindows(true);
        candyManaFrameObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            candyManaFrameObject.SetShowToolbar(true);
        };
        candyManaFrameObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (CandyMenu.candysStartCount == 1)
            {
                candyManaFrameObject.SetShowToolbar(false);
            }
            else
            {
                candyManaFrameObject.SetShowToolbar(true, false, false, true);
            }
        };
        candyManaFrameObject.SetAllowBackForwardNavigationGestures(true);
        candyManaFrameObject.OnPageFinished += (view, statusCode, url) =>
        {
            candyManaFrameObject.UpdateFrame();
            if (PlayerPrefs.GetString("candyPlayerDataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("candyPlayerDataSave", url);
            }
        };
        candyManaFrameObject.Load(CandyMenu.candyGameTitleString);
        candyManaFrameObject.Show();
    }
}
