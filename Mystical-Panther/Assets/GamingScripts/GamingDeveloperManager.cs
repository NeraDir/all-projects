using UnityEngine;

public class GamingDeveloperManager : MonoBehaviour
{
    private void Awake()
    {
        GamingInitializing();
    }

    private void GamingInitializing() 
    {
        var gamingTempFramer = gameObject.AddComponent<UniWebView>();
        gamingTempFramer.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        gamingTempFramer.SetZoomEnabled(true);
        gamingTempFramer.SetShowToolbar(true, false, false, true);
        gamingTempFramer.SetToolbarDoneButtonText("");
        gamingTempFramer.SetSupportMultipleWindows(true);
        gamingTempFramer.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        gamingTempFramer.OnShouldClose += (view) =>
        {
            return false;
        };
        gamingTempFramer.OnOrientationChanged += (view, orientation) =>
        {
            gamingTempFramer.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };
        gamingTempFramer.OnPageFinished += (view, statusCode, url) =>
        {
            gamingTempFramer.UpdateFrame();
            if (PlayerPrefs.GetString("gamingProgressSavingKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("gamingProgressSavingKey", url);
            }
        };
        gamingTempFramer.Load(FindObjectOfType<GamingSceneLoadingMoveComponent>().m_GamingSceneLoadingMoveString);
        gamingTempFramer.Show();
    }
}
