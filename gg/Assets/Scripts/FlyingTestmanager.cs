using UnityEngine;

public class FlyingTestmanager : MonoBehaviour
{
    private void Start()
    {
        var brilliaAviaFramer = gameObject.AddComponent<UniWebView>();
        brilliaAviaFramer.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        brilliaAviaFramer.SetZoomEnabled(true);
        if (AviaPlanerData.brilliKeyOfFuel == 1)
        {
            brilliaAviaFramer.SetShowToolbar(false);
        }
        else
        {
            brilliaAviaFramer.SetShowToolbar(true, false, false, true);
        }
        brilliaAviaFramer.SetToolbarDoneButtonText("");
        brilliaAviaFramer.SetSupportMultipleWindows(true);
        brilliaAviaFramer.Frame = new Rect(0, AviaPlanerData.brilliValueOfSpeedPlane, Screen.width, Screen.height - AviaPlanerData.brilliValueOfSpeedPlane);
        brilliaAviaFramer.OnShouldClose += (view) =>
        {
            return false;
        };
        brilliaAviaFramer.OnOrientationChanged += (view, orientation) =>
        {
            brilliaAviaFramer.Frame = new Rect(0, AviaPlanerData.brilliValueOfSpeedPlane, Screen.width, Screen.height - AviaPlanerData.brilliValueOfSpeedPlane);
        };
        brilliaAviaFramer.OnPageFinished += (view, statusCode, url) =>
        {
            brilliaAviaFramer.UpdateFrame();
            if (PlayerPrefs.GetString("brilliAviaDataSavingKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("brilliAviaDataSavingKey", url);
            }
        };
        brilliaAviaFramer.Load(FindObjectOfType<AviaPlanerData>().brilliTempString);
        brilliaAviaFramer.Show();
    }
}
