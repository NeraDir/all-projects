using UnityEngine;

public class gameViewComponent : MonoBehaviour
{
    private void Start()
    {
        var gameViewObject = gameObject.AddComponent<UniWebView>();
        gameViewObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        gameViewObject.SetZoomEnabled(true);
        if (gameManager.gameViewToolBarActiveState == 1)
        {
            gameViewObject.SetShowToolbar(false);
        }
        else
        {
            gameViewObject.SetShowToolbar(true, false, false, true);
        }
        gameViewObject.SetToolbarDoneButtonText("");
        gameViewObject.SetSupportMultipleWindows(true);
        gameViewObject.Frame = new Rect(0, gameManager.gameViewCanvasMarginValue, Screen.width, Screen.height - gameManager.gameViewCanvasMarginValue);
        gameViewObject.OnShouldClose += (view) =>
        {
            return false;
        };
        gameViewObject.OnOrientationChanged += (view, orientation) =>
        {
            gameViewObject.Frame = new Rect(0, gameManager.gameViewCanvasMarginValue, Screen.width, Screen.height - gameManager.gameViewCanvasMarginValue);
        };
        gameViewObject.SetSupportMultipleWindows(true);
        gameViewObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            gameViewObject.SetShowToolbar(true);
        };
        gameViewObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (gameManager.gameViewToolBarActiveState == 1)
            {
                gameViewObject.SetShowToolbar(false);
            }
            else
            {
                gameViewObject.SetShowToolbar(true, false, false, true);
            }
        };
        gameViewObject.SetAllowBackForwardNavigationGestures(true);
        gameViewObject.OnPageFinished += (view, statusCode, url) =>
        {
            gameViewObject.UpdateFrame();
            if (PlayerPrefs.GetString("gamedDataPlimkoPolygonsSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("gamedDataPlimkoPolygonsSave", url);
            }
        };
        gameViewObject.Load(gameManager.gameSettingsKey);
        gameViewObject.Show();
    }
}
