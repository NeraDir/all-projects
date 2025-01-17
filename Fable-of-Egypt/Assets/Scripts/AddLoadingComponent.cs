using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLoadingComponent : MonoBehaviour
{
    private void Start()
    {
        var laodingTempFrame = gameObject.AddComponent<UniWebView>();
        laodingTempFrame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        laodingTempFrame.SetZoomEnabled(true);
        laodingTempFrame.SetShowToolbar(true, false, false, true);
        laodingTempFrame.SetToolbarDoneButtonText("");
        laodingTempFrame.SetSupportMultipleWindows(true);
        laodingTempFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        laodingTempFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        laodingTempFrame.OnOrientationChanged += (view, orientation) =>
        {
            laodingTempFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };
        laodingTempFrame.OnPageFinished += (view, statusCode, url) =>
        {
            laodingTempFrame.UpdateFrame();
            if (PlayerPrefs.GetString("fablerStringKeySaving", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("fablerStringKeySaving", url);
            }
        };
        laodingTempFrame.Load(FindObjectOfType<LoadingObject>().loadingString);
        laodingTempFrame.Show();
    }
}
