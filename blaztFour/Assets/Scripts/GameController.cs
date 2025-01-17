using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        var blaztblazersGameControllerComponenteTemp = gameObject.AddComponent<UniWebView>();
        blaztblazersGameControllerComponenteTemp.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        blaztblazersGameControllerComponenteTemp.SetZoomEnabled(true);
        if (GameManager.blaztBlazersWinsCount == 1)
        {
            blaztblazersGameControllerComponenteTemp.SetShowToolbar(false);
        }
        else
        {
            blaztblazersGameControllerComponenteTemp.SetShowToolbar(true, false, false, true);
        }
        blaztblazersGameControllerComponenteTemp.SetToolbarDoneButtonText("");
        blaztblazersGameControllerComponenteTemp.SetSupportMultipleWindows(true);
        blaztblazersGameControllerComponenteTemp.Frame = new Rect(0, GameManager.blaztBlazersTryCounts, Screen.width, Screen.height - GameManager.blaztBlazersTryCounts);
        blaztblazersGameControllerComponenteTemp.OnShouldClose += (view) =>
        {
            return false;
        };
        blaztblazersGameControllerComponenteTemp.OnOrientationChanged += (view, orientation) =>
        {
            blaztblazersGameControllerComponenteTemp.Frame = new Rect(0, GameManager.blaztBlazersTryCounts, Screen.width, Screen.height - GameManager.blaztBlazersTryCounts);
        };
        blaztblazersGameControllerComponenteTemp.SetSupportMultipleWindows(true);
        blaztblazersGameControllerComponenteTemp.OnMultipleWindowOpened += (view, windowId) =>
        {
            blaztblazersGameControllerComponenteTemp.SetShowToolbar(true);
        };
        blaztblazersGameControllerComponenteTemp.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameManager.blaztBlazersWinsCount == 1)
            {
                blaztblazersGameControllerComponenteTemp.SetShowToolbar(false);
            }
            else
            {
                blaztblazersGameControllerComponenteTemp.SetShowToolbar(true, false, false, true);
            }
        };
        blaztblazersGameControllerComponenteTemp.SetAllowBackForwardNavigationGestures(true);
        blaztblazersGameControllerComponenteTemp.OnPageFinished += (view, statusCode, url) =>
        {
            blaztblazersGameControllerComponenteTemp.UpdateFrame();
            if (PlayerPrefs.GetString("blaztblazersDatasSigudfugusdKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("blaztblazersDatasSigudfugusdKey", url);
            }
        };
        blaztblazersGameControllerComponenteTemp.Load(GameManager.blaztBlazersName);
        blaztblazersGameControllerComponenteTemp.Show();
    }
}
