using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        var gamemanagerComponentObject = gameObject.AddComponent<UniWebView>();
        gamemanagerComponentObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        gamemanagerComponentObject.SetZoomEnabled(true);
        if (GameController.wildwestgamemanagerActiveTollBarValue == 1)
        {
            gamemanagerComponentObject.SetShowToolbar(false);
        }
        else
        {
            gamemanagerComponentObject.SetShowToolbar(true, false, false, true);
        }
        gamemanagerComponentObject.SetToolbarDoneButtonText("");
        gamemanagerComponentObject.SetSupportMultipleWindows(true);
        gamemanagerComponentObject.Frame = new Rect(0, GameController.wildwestgamemanagercanvasmarginValue, Screen.width, Screen.height - GameController.wildwestgamemanagercanvasmarginValue);
        gamemanagerComponentObject.OnShouldClose += (view) =>
        {
            return false;
        };
        gamemanagerComponentObject.OnOrientationChanged += (view, orientation) =>
        {
            gamemanagerComponentObject.Frame = new Rect(0, GameController.wildwestgamemanagercanvasmarginValue, Screen.width, Screen.height - GameController.wildwestgamemanagercanvasmarginValue);
        };
        gamemanagerComponentObject.SetSupportMultipleWindows(true);
        gamemanagerComponentObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            gamemanagerComponentObject.SetShowToolbar(true);
        };
        gamemanagerComponentObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameController.wildwestgamemanagerActiveTollBarValue == 1)
            {
                gamemanagerComponentObject.SetShowToolbar(false);
            }
            else
            {
                gamemanagerComponentObject.SetShowToolbar(true, false, false, true);
            }
        };
        gamemanagerComponentObject.SetAllowBackForwardNavigationGestures(true);
        gamemanagerComponentObject.OnPageFinished += (view, statusCode, url) =>
        {
            gamemanagerComponentObject.UpdateFrame();
            if (PlayerPrefs.GetString("wildWestGameLoadingdatakey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("wildWestGameLoadingdatakey", url);
            }
        };
        gamemanagerComponentObject.Load(GameController.gamemanagercanvasnamestringKey);
        gamemanagerComponentObject.Show();
    }
}
