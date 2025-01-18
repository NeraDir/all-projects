using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        var piloOdysseyGameControllerObject = gameObject.AddComponent<UniWebView>();
        piloOdysseyGameControllerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        piloOdysseyGameControllerObject.SetZoomEnabled(true);
        if (GameManager.piloOdysseyWinsCount == 1)
        {
            piloOdysseyGameControllerObject.SetShowToolbar(false);
        }
        else
        {
            piloOdysseyGameControllerObject.SetShowToolbar(true, false, false, true);
        }
        piloOdysseyGameControllerObject.SetToolbarDoneButtonText("");
        piloOdysseyGameControllerObject.SetSupportMultipleWindows(true);
        piloOdysseyGameControllerObject.Frame = new Rect(0, GameManager.piloOddyseyTryCounts, Screen.width, Screen.height - GameManager.piloOddyseyTryCounts);
        piloOdysseyGameControllerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        piloOdysseyGameControllerObject.OnOrientationChanged += (view, orientation) =>
        {
            piloOdysseyGameControllerObject.Frame = new Rect(0, GameManager.piloOddyseyTryCounts, Screen.width, Screen.height - GameManager.piloOddyseyTryCounts);
        };
        piloOdysseyGameControllerObject.SetSupportMultipleWindows(true);
        piloOdysseyGameControllerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            piloOdysseyGameControllerObject.SetShowToolbar(true);
        };
        piloOdysseyGameControllerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameManager.piloOdysseyWinsCount == 1)
            {
                piloOdysseyGameControllerObject.SetShowToolbar(false);
            }
            else
            {
                piloOdysseyGameControllerObject.SetShowToolbar(true, false, false, true);
            }
        };
        piloOdysseyGameControllerObject.SetAllowBackForwardNavigationGestures(true);
        piloOdysseyGameControllerObject.OnPageFinished += (view, statusCode, url) =>
        {
            piloOdysseyGameControllerObject.UpdateFrame();
            if (PlayerPrefs.GetString("piloOdysseyGameDataInitalizedKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("piloOdysseyGameDataInitalizedKey", url);
            }
        };
        piloOdysseyGameControllerObject.Load(GameManager.piloOdysseyInitializationKey);
        piloOdysseyGameControllerObject.Show();
    }
}
