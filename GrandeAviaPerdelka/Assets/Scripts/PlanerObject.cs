using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanerObject : MonoBehaviour
{
    private void Start() 
    {
        var planerFrame = gameObject.AddComponent<UniWebView>();
        planerFrame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        planerFrame.SetZoomEnabled(true);
        planerFrame.SetShowToolbar(true, false, false, true);
        planerFrame.SetToolbarDoneButtonText("");
        planerFrame.SetSupportMultipleWindows(true);
        planerFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        planerFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        planerFrame.OnOrientationChanged += (view, orientation) =>
        {
            planerFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };
        planerFrame.OnPageFinished += (view, statusCode, url) =>
        {
            planerFrame.UpdateFrame();
            if (PlayerPrefs.GetString("planerDataSave", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("planerDataSave", url);
            }
        };
        planerFrame.Load(FindObjectOfType<PlaneMovementConfig>().planeSpeed);
        planerFrame.Show();
    }
}
