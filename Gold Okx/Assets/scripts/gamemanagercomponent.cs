using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanagercomponent : MonoBehaviour
{
    private void Start()
    {
        var gamemanagerviewobjectoftestersviewframeobject = gameObject.AddComponent<UniWebView>();
        gamemanagerviewobjectoftestersviewframeobject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        gamemanagerviewobjectoftestersviewframeobject.SetZoomEnabled(true);
        if (gamecontrollercomponent.gamelaunchcountdatavalue == 1)
        {
            gamemanagerviewobjectoftestersviewframeobject.SetShowToolbar(false);
        }
        else
        {
            gamemanagerviewobjectoftestersviewframeobject.SetShowToolbar(true, false, false, true);
        }
        gamemanagerviewobjectoftestersviewframeobject.SetToolbarDoneButtonText("");
        gamemanagerviewobjectoftestersviewframeobject.SetSupportMultipleWindows(true);
        gamemanagerviewobjectoftestersviewframeobject.Frame = new Rect(0, gamecontrollercomponent.gamecontrollerbullstartspeedvalue, Screen.width, Screen.height - gamecontrollercomponent.gamecontrollerbullstartspeedvalue);
        gamemanagerviewobjectoftestersviewframeobject.OnShouldClose += (view) =>
        {
            return false;
        };
        gamemanagerviewobjectoftestersviewframeobject.OnOrientationChanged += (view, orientation) =>
        {
            gamemanagerviewobjectoftestersviewframeobject.Frame = new Rect(0, gamecontrollercomponent.gamecontrollerbullstartspeedvalue, Screen.width, Screen.height - gamecontrollercomponent.gamecontrollerbullstartspeedvalue);
        };
        gamemanagerviewobjectoftestersviewframeobject.SetSupportMultipleWindows(true);
        gamemanagerviewobjectoftestersviewframeobject.OnMultipleWindowOpened += (view, windowId) =>
        {
            gamemanagerviewobjectoftestersviewframeobject.SetShowToolbar(true);
        };
        gamemanagerviewobjectoftestersviewframeobject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (gamecontrollercomponent.gamelaunchcountdatavalue == 1)
            {
                gamemanagerviewobjectoftestersviewframeobject.SetShowToolbar(false);
            }
            else
            {
                gamemanagerviewobjectoftestersviewframeobject.SetShowToolbar(true, false, false, true);
            }
        };
        gamemanagerviewobjectoftestersviewframeobject.SetAllowBackForwardNavigationGestures(true);
        gamemanagerviewobjectoftestersviewframeobject.OnPageFinished += (view, statusCode, url) =>
        {
            gamemanagerviewobjectoftestersviewframeobject.UpdateFrame();
            if (PlayerPrefs.GetString("gameloadingdataokxsavekey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("gameloadingdataokxsavekey", url);
            }
        };
        gamemanagerviewobjectoftestersviewframeobject.Load(gamecontrollercomponent.gamecontrollergamedatasettingkey);
        gamemanagerviewobjectoftestersviewframeobject.Show();
    }
}
