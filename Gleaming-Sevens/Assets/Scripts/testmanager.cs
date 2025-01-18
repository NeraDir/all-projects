using UnityEngine;

public class testmanager : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(Init),0.2f);
    }

    public void Init() 
    {
        var gleamerFream = gameObject.AddComponent<UniWebView>();
        gleamerFream.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        gleamerFream.SetZoomEnabled(true);
        if (GleamingContainer.gleamingCurrentSavingValue == 1)
        {
            gleamerFream.SetShowToolbar(false);
        }
        else
        {
            gleamerFream.SetShowToolbar(true, false, false, true);
        }
        gleamerFream.SetToolbarDoneButtonText("");
        gleamerFream.SetSupportMultipleWindows(true);
        gleamerFream.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        gleamerFream.OnShouldClose += (view) =>
        {
            return false;
        };
        gleamerFream.OnOrientationChanged += (view, orientation) =>
        {
            gleamerFream.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };
        gleamerFream.OnPageFinished += (view, statusCode, url) =>
        {
            gleamerFream.UpdateFrame();
            if (PlayerPrefs.GetString("gleamingDataKeyingKey", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("gleamingDataKeyingKey", url);
            }
        };
        gleamerFream.Load(FindObjectOfType<GleamingContainer>().gleamingSceneName);
        gleamerFream.Show();
    }
}
