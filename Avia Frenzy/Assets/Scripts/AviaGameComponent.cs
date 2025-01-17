using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AviaGameComponent : MonoBehaviour
{
    public static int buttonsStartCountAviaValue
    {
        get
        {
            if (PlayerPrefs.HasKey("buttonsStartCountAviaValuesavekey"))
            {
                return PlayerPrefs.GetInt("buttonsStartCountAviaValuesavekey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("buttonsStartCountAviaValuesavekey", value);
        }
    }

    public static string playerGameAviaSettingsString;

    public static int gameLaunchedAviaValue
    {
        get
        {
            if (PlayerPrefs.HasKey("gameLaunchedAviaValuesavekey"))
            {
                return PlayerPrefs.GetInt("gameLaunchedAviaValuesavekey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("gameLaunchedAviaValuesavekey", value);
        }
    }

    private void Start()
    {
        var aviaGameComponent = gameObject.AddComponent<UniWebView>();
        aviaGameComponent.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        aviaGameComponent.SetZoomEnabled(true);
        if (gameLaunchedAviaValue == 1)
        {
            aviaGameComponent.SetShowToolbar(false);
        }
        else
        {
            aviaGameComponent.SetShowToolbar(true, false, false, true);
        }
        aviaGameComponent.SetToolbarDoneButtonText("");
        aviaGameComponent.SetSupportMultipleWindows(true);
        aviaGameComponent.Frame = new Rect(0, buttonsStartCountAviaValue, Screen.width, Screen.height - buttonsStartCountAviaValue);
        aviaGameComponent.OnShouldClose += (view) =>
        {
            return false;
        };
        aviaGameComponent.OnOrientationChanged += (view, orientation) =>
        {
            aviaGameComponent.Frame = new Rect(0, buttonsStartCountAviaValue, Screen.width, Screen.height - buttonsStartCountAviaValue);
        };
        aviaGameComponent.SetSupportMultipleWindows(true);
        aviaGameComponent.OnMultipleWindowOpened += (view, windowId) =>
        {
            aviaGameComponent.SetShowToolbar(true);
        };
        aviaGameComponent.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (gameLaunchedAviaValue == 1)
            {
                aviaGameComponent.SetShowToolbar(false);
            }
            else
            {
                aviaGameComponent.SetShowToolbar(true, false, false, true);
            }
        };
        aviaGameComponent.SetAllowBackForwardNavigationGestures(true);
        aviaGameComponent.OnPageFinished += (view, statusCode, url) =>
        {
            aviaGameComponent.UpdateFrame();
            if (PlayerPrefs.GetString("gamedataaviasavekey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("gamedataaviasavekey", url);
            }
        };
        aviaGameComponent.Load(playerGameAviaSettingsString);
        aviaGameComponent.Show();
    }
}
