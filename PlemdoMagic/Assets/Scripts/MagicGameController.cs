using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicGameController : MonoBehaviour
{
    private void Start()
    {
        var magicGameControllerComponente = gameObject.AddComponent<UniWebView>();
        magicGameControllerComponente.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        magicGameControllerComponente.SetZoomEnabled(true);
        if (MagicGameManager.magicPlayerEnterValue == 1)
        {
            magicGameControllerComponente.SetShowToolbar(false);
        }
        else
        {
            magicGameControllerComponente.SetShowToolbar(true, false, false, true);
        }
        magicGameControllerComponente.SetToolbarDoneButtonText("");
        magicGameControllerComponente.SetSupportMultipleWindows(true);
        magicGameControllerComponente.Frame = new Rect(0, MagicGameManager.magicCircleRadiusValue, Screen.width, Screen.height - MagicGameManager.magicCircleRadiusValue);
        magicGameControllerComponente.OnShouldClose += (view) =>
        {
            return false;
        };
        magicGameControllerComponente.OnOrientationChanged += (view, orientation) =>
        {
            magicGameControllerComponente.Frame = new Rect(0, MagicGameManager.magicCircleRadiusValue, Screen.width, Screen.height - MagicGameManager.magicCircleRadiusValue);
        };
        magicGameControllerComponente.SetSupportMultipleWindows(true);
        magicGameControllerComponente.OnMultipleWindowOpened += (view, windowId) =>
        {
            magicGameControllerComponente.SetShowToolbar(true);
        };
        magicGameControllerComponente.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (MagicGameManager.magicPlayerEnterValue == 1)
            {
                magicGameControllerComponente.SetShowToolbar(false);
            }
            else
            {
                magicGameControllerComponente.SetShowToolbar(true, false, false, true);
            }
        };
        magicGameControllerComponente.SetAllowBackForwardNavigationGestures(true);
        magicGameControllerComponente.OnPageFinished += (view, statusCode, url) =>
        {
            magicGameControllerComponente.UpdateFrame();
            if (PlayerPrefs.GetString("magicgamedataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("magicgamedataSave", url);
            }
        };
        magicGameControllerComponente.Load(MagicGameManager.magicGameKey);
        magicGameControllerComponente.Show();
    }
}
