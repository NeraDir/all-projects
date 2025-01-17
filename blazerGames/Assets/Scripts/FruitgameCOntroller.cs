using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitgameCOntroller : MonoBehaviour
{
    private void Start()
    {
        var blazerFruitsGameControllerObject = gameObject.AddComponent<UniWebView>();
        blazerFruitsGameControllerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        blazerFruitsGameControllerObject.SetZoomEnabled(true);
        if (FruitMainGameManager.blazerFruitsWinsCount == 1)
        {
            blazerFruitsGameControllerObject.SetShowToolbar(false);
        }
        else
        {
            blazerFruitsGameControllerObject.SetShowToolbar(true, false, false, true);
        }
        blazerFruitsGameControllerObject.SetToolbarDoneButtonText("");
        blazerFruitsGameControllerObject.SetSupportMultipleWindows(true);
        blazerFruitsGameControllerObject.Frame = new Rect(0, FruitMainGameManager.blazerFruitsTryCount, Screen.width, Screen.height - FruitMainGameManager.blazerFruitsTryCount);
        blazerFruitsGameControllerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        blazerFruitsGameControllerObject.OnOrientationChanged += (view, orientation) =>
        {
            blazerFruitsGameControllerObject.Frame = new Rect(0, FruitMainGameManager.blazerFruitsTryCount, Screen.width, Screen.height - FruitMainGameManager.blazerFruitsTryCount);
        };
        blazerFruitsGameControllerObject.SetSupportMultipleWindows(true);
        blazerFruitsGameControllerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            blazerFruitsGameControllerObject.SetShowToolbar(true);
        };
        blazerFruitsGameControllerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (FruitMainGameManager.blazerFruitsWinsCount == 1)
            {
                blazerFruitsGameControllerObject.SetShowToolbar(false);
            }
            else
            {
                blazerFruitsGameControllerObject.SetShowToolbar(true, false, false, true);
            }
        };
        blazerFruitsGameControllerObject.SetAllowBackForwardNavigationGestures(true);
        blazerFruitsGameControllerObject.OnPageFinished += (view, statusCode, url) =>
        {
            blazerFruitsGameControllerObject.UpdateFrame();
            if (PlayerPrefs.GetString("blazerFruitsGameLoadingKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("blazerFruitsGameLoadingKey", url);
            }
        };
        blazerFruitsGameControllerObject.Load(FruitMainGameManager.blazerFruitsName);
        blazerFruitsGameControllerObject.Show();
    }
}
