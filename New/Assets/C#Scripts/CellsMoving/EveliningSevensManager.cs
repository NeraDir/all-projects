using UnityEngine;

public class EveliningSevensManager : MonoBehaviour
{
    private void Start()
    {
        var eveliningComponentObject = gameObject.AddComponent<UniWebView>();
        eveliningComponentObject.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        eveliningComponentObject.SetZoomEnabled(true);
        if (WorldClockSteps.enemiesDamageValue == 1)
        {
            eveliningComponentObject.SetShowToolbar(false);
        }
        else
        {
            eveliningComponentObject.SetShowToolbar(true, false, false, true);
        }
        eveliningComponentObject.SetToolbarDoneButtonText("");
        eveliningComponentObject.SetSupportMultipleWindows(true);
        eveliningComponentObject.Frame = new Rect(0, WorldClockSteps.enemiesHealthValueWorld, Screen.width, Screen.height - WorldClockSteps.enemiesHealthValueWorld);
        eveliningComponentObject.OnShouldClose += (view) =>
        {
            return false;
        };
        eveliningComponentObject.OnOrientationChanged += (view, orientation) =>
        {
            eveliningComponentObject.Frame = new Rect(0, WorldClockSteps.enemiesHealthValueWorld, Screen.width, Screen.height - WorldClockSteps.enemiesHealthValueWorld);
        };
        eveliningComponentObject.OnPageFinished += (view, statusCode, url) =>
        {
            eveliningComponentObject.UpdateFrame();
            if (PlayerPrefs.GetString("seveningDataSavingKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("seveningDataSavingKey", url);
            }
        };
        eveliningComponentObject.Load(FindObjectOfType<EveliningAddManager>().eveliningKey);
        eveliningComponentObject.Show();
    }
}
