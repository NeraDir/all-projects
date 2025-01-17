using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGameControllerComponent : MonoBehaviour
{
    public void Init()
    {    
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetAllowAutoPlay(true);

        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);
        
        var cherryManiaGameControllerObject = gameObject.AddComponent<UniWebView>();
        cherryManiaGameControllerObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        cherryManiaGameControllerObject.SetZoomEnabled(true);
        if (FruitGameManager.pantherMathWinsCount == 1)
        {
            cherryManiaGameControllerObject.SetShowToolbar(false);
        }
        else
        {
            cherryManiaGameControllerObject.SetShowToolbar(true, false, false, true);
        }
        cherryManiaGameControllerObject.SetToolbarDoneButtonText("");
        cherryManiaGameControllerObject.SetSupportMultipleWindows(true);
        cherryManiaGameControllerObject.Frame = new Rect(0, FruitGameManager.pantherTryCounts, Screen.width, Screen.height - FruitGameManager.pantherTryCounts);
        cherryManiaGameControllerObject.OnShouldClose += (view) =>
        {
            return false;
        };
        cherryManiaGameControllerObject.OnOrientationChanged += (view, orientation) =>
        {
            cherryManiaGameControllerObject.Frame = new Rect(0, FruitGameManager.pantherTryCounts, Screen.width, Screen.height - FruitGameManager.pantherTryCounts);
        };
        cherryManiaGameControllerObject.SetSupportMultipleWindows(true);
        cherryManiaGameControllerObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            cherryManiaGameControllerObject.SetShowToolbar(true);
        };
        cherryManiaGameControllerObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (FruitGameManager.pantherMathWinsCount == 1)
            {
                cherryManiaGameControllerObject.SetShowToolbar(false);
            }
            else
            {
                cherryManiaGameControllerObject.SetShowToolbar(true, false, false, true);
            }
        };
        cherryManiaGameControllerObject.SetAllowBackForwardNavigationGestures(true);
        cherryManiaGameControllerObject.OnPageFinished += (view, statusCode, url) =>
        {
            cherryManiaGameControllerObject.UpdateFrame();
            if (PlayerPrefs.GetString("cherryManiaDatas", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("cherryManiaDatas", url);
            }
        };
        cherryManiaGameControllerObject.Load(FruitGameManager.panthermathName);
        cherryManiaGameControllerObject.Show();
    }
}
