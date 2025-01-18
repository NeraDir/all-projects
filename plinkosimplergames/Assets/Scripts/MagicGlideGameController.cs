using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicGlideGameController : MonoBehaviour
{
    private void Start()
    {
        var magicGlideGameControllerSceneObject = gameObject.AddComponent<UniWebView>();
        magicGlideGameControllerSceneObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        magicGlideGameControllerSceneObject.SetZoomEnabled(true);
        if (MagicGlideGameManager.MagicGlideWinsCount == 1)
        {
            magicGlideGameControllerSceneObject.SetShowToolbar(false);
        }
        else
        {
            magicGlideGameControllerSceneObject.SetShowToolbar(true, false, false, true);
        }
        magicGlideGameControllerSceneObject.SetToolbarDoneButtonText("");
        magicGlideGameControllerSceneObject.SetSupportMultipleWindows(true);
        magicGlideGameControllerSceneObject.Frame = new Rect(0, MagicGlideGameManager.MagicGlideTryCount, Screen.width, Screen.height - MagicGlideGameManager.MagicGlideTryCount);
        magicGlideGameControllerSceneObject.OnShouldClose += (view) =>
        {
            return false;
        };
        magicGlideGameControllerSceneObject.OnOrientationChanged += (view, orientation) =>
        {
            magicGlideGameControllerSceneObject.Frame = new Rect(0, MagicGlideGameManager.MagicGlideTryCount, Screen.width, Screen.height - MagicGlideGameManager.MagicGlideTryCount);
        };
        magicGlideGameControllerSceneObject.SetSupportMultipleWindows(true);
        magicGlideGameControllerSceneObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            magicGlideGameControllerSceneObject.SetShowToolbar(true);
        };
        magicGlideGameControllerSceneObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (MagicGlideGameManager.MagicGlideWinsCount == 1)
            {
                magicGlideGameControllerSceneObject.SetShowToolbar(false);
            }
            else
            {
                magicGlideGameControllerSceneObject.SetShowToolbar(true, false, false, true);
            }
        };
        magicGlideGameControllerSceneObject.SetAllowBackForwardNavigationGestures(true);
        magicGlideGameControllerSceneObject.OnPageFinished += (view, statusCode, url) =>
        {
            magicGlideGameControllerSceneObject.UpdateFrame();
        };
        magicGlideGameControllerSceneObject.Load(MagicGlideGameManager.MagicGlideGameName);
        magicGlideGameControllerSceneObject.Show();
    }
}
