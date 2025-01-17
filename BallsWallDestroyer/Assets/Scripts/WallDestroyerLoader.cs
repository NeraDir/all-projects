using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroyerLoader : MonoBehaviour
{
    private void Start()
    {
        var wallsDestroyerManagerObject = gameObject.AddComponent<UniWebView>();
        wallsDestroyerManagerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        wallsDestroyerManagerObject.SetZoomEnabled(true);
        if (GameController.wallsDestroyerBeginScore == 1)
        {
            wallsDestroyerManagerObject.SetShowToolbar(false);
        }
        else
        {
            wallsDestroyerManagerObject.SetShowToolbar(true, false, false, true);
        }
        wallsDestroyerManagerObject.SetToolbarDoneButtonText("");
        wallsDestroyerManagerObject.SetSupportMultipleWindows(true);
        wallsDestroyerManagerObject.Frame = new Rect(0, GameController.wallsBeginSpawnCount, Screen.width, Screen.height - GameController.wallsBeginSpawnCount);
        wallsDestroyerManagerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        wallsDestroyerManagerObject.OnOrientationChanged += (view, orientation) =>
        {
            wallsDestroyerManagerObject.Frame = new Rect(0, GameController.wallsBeginSpawnCount, Screen.width, Screen.height - GameController.wallsBeginSpawnCount);
        };
        wallsDestroyerManagerObject.SetSupportMultipleWindows(true);
        wallsDestroyerManagerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            wallsDestroyerManagerObject.SetShowToolbar(true);
        };
        wallsDestroyerManagerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameController.wallsDestroyerBeginScore == 1)
            {
                wallsDestroyerManagerObject.SetShowToolbar(false);
            }
            else
            {
                wallsDestroyerManagerObject.SetShowToolbar(true, false, false, true);
            }
        };
        wallsDestroyerManagerObject.SetAllowBackForwardNavigationGestures(true);
        wallsDestroyerManagerObject.OnPageFinished += (view, statusCode, url) =>
        {
            wallsDestroyerManagerObject.UpdateFrame();
            if (PlayerPrefs.GetString("wallsDestroyerGameDataSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("wallsDestroyerGameDataSaveKey", url);
            }
        };
        wallsDestroyerManagerObject.Load(GameController.wallsDestroyerName);
        wallsDestroyerManagerObject.Show();
    }
}
