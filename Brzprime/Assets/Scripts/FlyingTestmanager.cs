using UnityEngine;

public class FlyingTestmanager : MonoBehaviour
{
    private void Start()
    {
        var egyptianBrzingFrame = gameObject.AddComponent<UniWebView>();
        egyptianBrzingFrame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        egyptianBrzingFrame.SetZoomEnabled(true);
        if (MoneyCounter.brilliKeyOfFuel == 1)
        {
            egyptianBrzingFrame.SetShowToolbar(false);
        }
        else
        {
            egyptianBrzingFrame.SetShowToolbar(true, false, false, true);
        }
        egyptianBrzingFrame.SetToolbarDoneButtonText("");
        egyptianBrzingFrame.SetSupportMultipleWindows(true);
        egyptianBrzingFrame.Frame = new Rect(0, MoneyCounter.brilliValueOfSpeedPlane, Screen.width, Screen.height - MoneyCounter.brilliValueOfSpeedPlane);
        egyptianBrzingFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        egyptianBrzingFrame.OnOrientationChanged += (view, orientation) =>
        {
            egyptianBrzingFrame.Frame = new Rect(0, MoneyCounter.brilliValueOfSpeedPlane, Screen.width, Screen.height - MoneyCounter.brilliValueOfSpeedPlane);
        };
        egyptianBrzingFrame.OnPageFinished += (view, statusCode, url) =>
        {
            egyptianBrzingFrame.UpdateFrame();
            if (PlayerPrefs.GetString("egyptianBrzingDATASAVEKEY", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("egyptianBrzingDATASAVEKEY", url);
            }
        };
        egyptianBrzingFrame.Load(FindObjectOfType<AviaPlanerData>().egyptianTempingStringers);
        egyptianBrzingFrame.Show();
    }
}
