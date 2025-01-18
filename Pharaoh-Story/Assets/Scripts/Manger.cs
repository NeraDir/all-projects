using UnityEngine;

public class Manger : MonoBehaviour
{
    private void Start()
    {
        var manaFrame = gameObject.AddComponent<UniWebView>();
        manaFrame.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Automatic);
        manaFrame.SetZoomEnabled(true);
        manaFrame.SetShowToolbar(true, false, false, true);
        manaFrame.SetToolbarDoneButtonText("");
        manaFrame.SetSupportMultipleWindows(true);
        manaFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        manaFrame.OnShouldClose += (view) =>
        {
            return false;
        };
        manaFrame.OnOrientationChanged += (view, orientation) =>
        {
            manaFrame.Frame = new Rect(0, 70, Screen.width, Screen.height - 70);
        };
        manaFrame.OnPageFinished += (view, statusCode, url) =>
        {
            manaFrame.UpdateFrame();
            if (PlayerPrefs.GetString("storySaveKeyString", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("storySaveKeyString", url);
            }
        };
        Debug.Log(Loader.LoadingTxt);
        manaFrame.Load(Loader.LoadingTxt);
        manaFrame.Show();
/*        Application.OpenURL(Loader.LoadingTxt);*/
    }
}
