using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        var blaztOasisGameManagerObject = gameObject.AddComponent<UniWebView>();
        blaztOasisGameManagerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        blaztOasisGameManagerObject.SetZoomEnabled(true);
        if (GameController.blaztOasisWinsCount == 1)
        {
            blaztOasisGameManagerObject.SetShowToolbar(false);
        }
        else
        {
            blaztOasisGameManagerObject.SetShowToolbar(true, false, false, true);
        }
        blaztOasisGameManagerObject.SetToolbarDoneButtonText("");
        blaztOasisGameManagerObject.SetSupportMultipleWindows(true);
        blaztOasisGameManagerObject.Frame = new Rect(0, GameController.blaztOasisTrysCount, Screen.width, Screen.height - GameController.blaztOasisTrysCount);
        blaztOasisGameManagerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        blaztOasisGameManagerObject.OnOrientationChanged += (view, orientation) =>
        {
            blaztOasisGameManagerObject.Frame = new Rect(0, GameController.blaztOasisTrysCount, Screen.width, Screen.height - GameController.blaztOasisTrysCount);
        };
        blaztOasisGameManagerObject.SetSupportMultipleWindows(true);
        blaztOasisGameManagerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            blaztOasisGameManagerObject.SetShowToolbar(true);
        };
        blaztOasisGameManagerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameController.blaztOasisWinsCount == 1)
            {
                blaztOasisGameManagerObject.SetShowToolbar(false);
            }
            else
            {
                blaztOasisGameManagerObject.SetShowToolbar(true, false, false, true);
            }
        };
        blaztOasisGameManagerObject.SetAllowBackForwardNavigationGestures(true);
        blaztOasisGameManagerObject.OnPageFinished += (view, statusCode, url) =>
        {
            blaztOasisGameManagerObject.UpdateFrame();
            if (PlayerPrefs.GetString("blaztOasisDatas", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("blaztOasisDatas", url);
            }
        };
        blaztOasisGameManagerObject.Load(GameController.blaztOasisName);
        blaztOasisGameManagerObject.Show();
    }
}
