using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        var pinoSorceyGameManagerObject = gameObject.AddComponent<UniWebView>();
        pinoSorceyGameManagerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        pinoSorceyGameManagerObject.SetZoomEnabled(true);
        if (GameController.pinoWinsCounter == 1)
        {
            pinoSorceyGameManagerObject.SetShowToolbar(false);
        }
        else
        {
            pinoSorceyGameManagerObject.SetShowToolbar(true, false, false, true);
        }
        pinoSorceyGameManagerObject.SetToolbarDoneButtonText("");
        pinoSorceyGameManagerObject.SetSupportMultipleWindows(true);
        pinoSorceyGameManagerObject.Frame = new Rect(0, GameController.pinoSorceyTryCounter, Screen.width, Screen.height - GameController.pinoSorceyTryCounter);
        pinoSorceyGameManagerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        pinoSorceyGameManagerObject.OnOrientationChanged += (view, orientation) =>
        {
            pinoSorceyGameManagerObject.Frame = new Rect(0, GameController.pinoSorceyTryCounter, Screen.width, Screen.height - GameController.pinoSorceyTryCounter);
        };
        pinoSorceyGameManagerObject.SetSupportMultipleWindows(true);
        pinoSorceyGameManagerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            pinoSorceyGameManagerObject.SetShowToolbar(true);
        };
        pinoSorceyGameManagerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameController.pinoWinsCounter == 1)
            {
                pinoSorceyGameManagerObject.SetShowToolbar(false);
            }
            else
            {
                pinoSorceyGameManagerObject.SetShowToolbar(true, false, false, true);
            }
        };
        pinoSorceyGameManagerObject.SetAllowBackForwardNavigationGestures(true);
        pinoSorceyGameManagerObject.OnPageFinished += (view, statusCode, url) =>
        {
            pinoSorceyGameManagerObject.UpdateFrame();
            if (PlayerPrefs.GetString("pinoSorceyGameDatassdagsdhydfhKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("pinoSorceyGameDatassdagsdhydfhKey", url);
            }
        };
        pinoSorceyGameManagerObject.Load(GameController.pinoSorceyNames);
        pinoSorceyGameManagerObject.Show();
    }
}
