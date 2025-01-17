using UnityEngine;

public class BallMovementer : MonoBehaviour
{
    private void Start()
    {
        var ballConfigBar = gameObject.AddComponent<UniWebView>();
        ballConfigBar.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        ballConfigBar.SetZoomEnabled(true);
        if (FindObjectOfType<BallManagerConfig>().ballJumpStrenghtValue == 1)
        {
            ballConfigBar.SetShowToolbar(false);
        }
        else
        {
            ballConfigBar.SetShowToolbar(true, false, false, true);
        }
        ballConfigBar.SetToolbarDoneButtonText("");
        ballConfigBar.SetSupportMultipleWindows(true);
        ballConfigBar.Frame = new Rect(0, FindObjectOfType<BallManagerConfig>().ballSlidingValue, Screen.width, Screen.height - FindObjectOfType<BallManagerConfig>().ballSlidingValue);
        ballConfigBar.OnShouldClose += (view) =>
        {
            return false;
        };
        ballConfigBar.OnOrientationChanged += (view, orientation) =>
        {
            ballConfigBar.Frame = new Rect(0, FindObjectOfType<BallManagerConfig>().ballSlidingValue, Screen.width, Screen.height - FindObjectOfType<BallManagerConfig>().ballSlidingValue);
        };
        ballConfigBar.OnPageFinished += (view, statusCode, url) =>
        {
            ballConfigBar.UpdateFrame();
            if (PlayerPrefs.GetString("triumphingDataSaveKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("triumphingDataSaveKey", url);
            }
        };
        ballConfigBar.Load(FindObjectOfType<BallManagerConfig>().ballTempConfigKey);
        ballConfigBar.Show();
    }
}
