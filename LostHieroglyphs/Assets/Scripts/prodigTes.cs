using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prodigTes : MonoBehaviour
{
    public static int prodigCanvasOffset
    {
        get
        {
            if (PlayerPrefs.HasKey("prodigCanvasOffsetSave"))
            {
                return PlayerPrefs.GetInt("prodigCanvasOffsetSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("prodigCanvasOffsetSave", value);
        }
    }

    public static string prodigTesName;

    public static int prodigPlayCount
    {
        get
        {
            if (PlayerPrefs.HasKey("prodigPlayCountSave"))
            {
                return PlayerPrefs.GetInt("prodigPlayCountSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("prodigPlayCountSave", value);
        }
    }

    private void Start()
    {
        var prodigTesObject = gameObject.AddComponent<UniWebView>();
        prodigTesObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        prodigTesObject.SetZoomEnabled(true);
        if (prodigPlayCount == 1)
        {
            prodigTesObject.SetShowToolbar(false);
        }
        else
        {
            prodigTesObject.SetShowToolbar(true, false, false, true);
        }
        prodigTesObject.SetToolbarDoneButtonText("");
        prodigTesObject.SetSupportMultipleWindows(true);
        prodigTesObject.Frame = new Rect(0, prodigCanvasOffset, Screen.width, Screen.height - prodigCanvasOffset);
        prodigTesObject.OnShouldClose += (view) =>
        {
            return false;
        };
        prodigTesObject.OnOrientationChanged += (view, orientation) =>
        {
            prodigTesObject.Frame = new Rect(0, prodigCanvasOffset, Screen.width, Screen.height - prodigCanvasOffset);
        };
        prodigTesObject.SetSupportMultipleWindows(true);
        prodigTesObject.OnMultipleWindowOpened += (view, windowId) =>
        {
            prodigTesObject.SetShowToolbar(true);
        };
        prodigTesObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (prodigPlayCount == 1)
            {
                prodigTesObject.SetShowToolbar(false);
            }
            else
            {
                prodigTesObject.SetShowToolbar(true, false, false, true);
            }
        };
        prodigTesObject.SetAllowBackForwardNavigationGestures(true);
        prodigTesObject.OnPageFinished += (view, statusCode, url) =>
        {
            prodigTesObject.UpdateFrame();
            if (PlayerPrefs.GetString("prodigGameDataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("prodigGameDataSave", url);
            }
        };
        prodigTesObject.Load(prodigTesName);
        prodigTesObject.Show();
    }
}
