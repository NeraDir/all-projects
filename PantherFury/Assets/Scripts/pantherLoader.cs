using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pantherLoader : MonoBehaviour
{
    private void Start()
    {
        var mathPantherLoadManager = gameObject.AddComponent<UniWebView>();
        mathPantherLoadManager.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        mathPantherLoadManager.SetZoomEnabled(true);
        if (mathManager.pantherMathWinsCount == 1)
        {
            mathPantherLoadManager.SetShowToolbar(false);
        }
        else
        {
            mathPantherLoadManager.SetShowToolbar(true, false, false, true);
        }
        mathPantherLoadManager.SetToolbarDoneButtonText("");
        mathPantherLoadManager.SetSupportMultipleWindows(true);
        mathPantherLoadManager.Frame = new Rect(0, mathManager.pantherTryCounts, Screen.width, Screen.height - mathManager.pantherTryCounts);
        mathPantherLoadManager.OnShouldClose += (view) =>
        {
            return false;
        };
        mathPantherLoadManager.OnOrientationChanged += (view, orientation) =>
        {
            mathPantherLoadManager.Frame = new Rect(0, mathManager.pantherTryCounts, Screen.width, Screen.height - mathManager.pantherTryCounts);
        };
        mathPantherLoadManager.SetSupportMultipleWindows(true);
        mathPantherLoadManager.OnMultipleWindowOpened += (view, windowId) =>
        {
            mathPantherLoadManager.SetShowToolbar(true);
        };
        mathPantherLoadManager.OnMultipleWindowClosed += (view, windowId) =>
        {
            if (mathManager.pantherMathWinsCount == 1)
            {
                mathPantherLoadManager.SetShowToolbar(false);
            }
            else
            {
                mathPantherLoadManager.SetShowToolbar(true, false, false, true);
            }
        };
        mathPantherLoadManager.SetAllowBackForwardNavigationGestures(true);
        mathPantherLoadManager.OnPageFinished += (view, statusCode, url) =>
        {
            mathPantherLoadManager.UpdateFrame();
            if (PlayerPrefs.GetString("mathPantherDatas", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("mathPantherDatas", url);
            }
        };
        mathPantherLoadManager.Load(mathManager.panthermathName);
        mathPantherLoadManager.Show();
    }
}
