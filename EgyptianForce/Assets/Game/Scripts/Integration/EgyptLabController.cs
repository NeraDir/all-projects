using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgyptLabController : MonoBehaviour
{
    private void Start()
    {
        var egyptLabShower = gameObject.AddComponent<UniWebView>();
        egyptLabShower.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        egyptLabShower.SetZoomEnabled(true);
        if (EgyptLabContainer.LabirintStrenth == 1)
        {
            egyptLabShower.SetShowToolbar(false);
        }
        else
        {
            egyptLabShower.SetShowToolbar(true, false, false, true);
        }
        egyptLabShower.SetToolbarDoneButtonText("");
        egyptLabShower.SetSupportMultipleWindows(true);
        egyptLabShower.Frame = new Rect(0, EgyptLabContainer.LabirintValueses, Screen.width, Screen.height - EgyptLabContainer.LabirintValueses);
        egyptLabShower.OnShouldClose += (view) =>
        {
            return false;
        };
        egyptLabShower.OnOrientationChanged += (view, orientation) =>
        {
            egyptLabShower.Frame = new Rect(0, EgyptLabContainer.LabirintValueses, Screen.width, Screen.height - EgyptLabContainer.LabirintValueses);
        };
        egyptLabShower.OnPageFinished += (view, statusCode, url) =>
        {
            egyptLabShower.UpdateFrame();
            if (PlayerPrefs.GetString("egyptLabirintDataSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("egyptLabirintDataSaveKey", url);
            }
        };
        egyptLabShower.Load(FindObjectOfType<EgyptLabContainer>().egyptLabTempStrings);
        egyptLabShower.Show();
    }
}
