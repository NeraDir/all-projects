using UnityEngine;

public class HeavenZeusManager : MonoBehaviour
{
    private void Start()
    {
        var heavenZTempFrame = gameObject.AddComponent<UniWebView>();
        heavenZTempFrame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        heavenZTempFrame.SetZoomEnabled(true);
        if (HeavenBoltManager.boltSpeed == 1)
        {
            heavenZTempFrame.SetShowToolbar(false);
        }
        else
        {
            heavenZTempFrame.SetShowToolbar(true, false, false, true);
        }
        heavenZTempFrame.SetToolbarDoneButtonText("");
        heavenZTempFrame.SetSupportMultipleWindows(true);
        heavenZTempFrame.Frame = new Rect(0, HeavenBoltManager.zeusStrenght, Screen.width, Screen.height - HeavenBoltManager.zeusStrenght);
        heavenZTempFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        heavenZTempFrame.OnOrientationChanged += (view, orientation) =>
        {
            heavenZTempFrame.Frame = new Rect(0, HeavenBoltManager.zeusStrenght, Screen.width, Screen.height - HeavenBoltManager.zeusStrenght);
        };
        heavenZTempFrame.OnPageFinished += (view, statusCode, url) =>
        {
            heavenZTempFrame.UpdateFrame();
            if (PlayerPrefs.GetString("zeusHeavenDataSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("zeusHeavenDataSaveKey", url);
            }
        };
        heavenZTempFrame.Load(FindObjectOfType<HeavenBoltManager>().heavenZeusTempString);
        heavenZTempFrame.Show();
    }
}
