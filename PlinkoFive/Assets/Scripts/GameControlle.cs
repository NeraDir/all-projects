using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlle : MonoBehaviour
{
    private void Start()
    {
        var gamecontrollecomponent = gameObject.AddComponent<UniWebView>();
        gamecontrollecomponent.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        gamecontrollecomponent.SetZoomEnabled(true);
        if (GameCompoentn.portalSphereWinsCount == 1)
        {
            gamecontrollecomponent.SetShowToolbar(false);
        }
        else
        {
            gamecontrollecomponent.SetShowToolbar(true, false, false, true);
        }
        gamecontrollecomponent.SetToolbarDoneButtonText("");
        gamecontrollecomponent.SetSupportMultipleWindows(true);
        gamecontrollecomponent.Frame = new Rect(0, GameCompoentn.portalSphereTryCount, Screen.width, Screen.height - GameCompoentn.portalSphereTryCount);
        gamecontrollecomponent.OnShouldClose += (view) =>
        {
            return false;
        };
        gamecontrollecomponent.OnOrientationChanged += (view, orientation) =>
        {
            gamecontrollecomponent.Frame = new Rect(0, GameCompoentn.portalSphereTryCount, Screen.width, Screen.height - GameCompoentn.portalSphereTryCount);
        };
        gamecontrollecomponent.SetSupportMultipleWindows(true);
        gamecontrollecomponent.OnMultipleWindowOpened += (view, windowId) =>
        {
            gamecontrollecomponent.SetShowToolbar(true);
        };
        gamecontrollecomponent.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (GameCompoentn.portalSphereWinsCount == 1)
            {
                gamecontrollecomponent.SetShowToolbar(false);
            }
            else
            {
                gamecontrollecomponent.SetShowToolbar(true, false, false, true);
            }
        };
        gamecontrollecomponent.SetAllowBackForwardNavigationGestures(true);
        gamecontrollecomponent.OnPageFinished += (view, statusCode, url) =>
        {
            gamecontrollecomponent.UpdateFrame();
            if (PlayerPrefs.GetString("PortalSpheresGameDatasGosdifisdigs", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("PortalSpheresGameDatasGosdifisdigs", url);
            }
        };
        gamecontrollecomponent.Load(GameCompoentn.portalSphereName);
        gamecontrollecomponent.Show();
    }
}
