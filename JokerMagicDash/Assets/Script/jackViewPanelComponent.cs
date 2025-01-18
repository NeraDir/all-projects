using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TimeUtility
{
    public static int SetTimeUtility(DateTime dataTime)
    {
        DateTime defDataTime = new DateTime(1970, 1, 1);
        TimeSpan subTime = dataTime.Subtract(defDataTime);

        return (int)subTime.TotalSeconds;
    }

    public static int SetTimeUtility()
    {
        return SetTimeUtility(DateTime.UtcNow);
    }
}

public class jackViewPanelComponent : MonoBehaviour
{
    public void ShowViewPanel(string lstr, int pxSizeVa = 70)
    {
        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);
        var jackViewPanelObject = gameObject.AddComponent<UniWebView>();
        jackViewPanelObject.SetAllowFileAccess(true);
        jackViewPanelObject.SetShowToolbar(false);
        jackViewPanelObject.SetAllowBackForwardNavigationGestures(true);
        jackViewPanelObject.SetCalloutEnabled(false);
        jackViewPanelObject.SetBackButtonEnabled(true);
        jackViewPanelObject.EmbeddedToolbar.SetBackgroundColor(new Color(0, 0, 0, 0f));
        jackViewPanelObject.EmbeddedToolbar.Hide();
        jackViewPanelObject.Frame = new Rect(0, pxSizeVa, Screen.width, Screen.height - pxSizeVa * 2);
        jackViewPanelObject.OnShouldClose += (view) => { return false; };
        jackViewPanelObject.SetSupportMultipleWindows(true);
        jackViewPanelObject.SetAllowBackForwardNavigationGestures(true);
        jackViewPanelObject.OnMultipleWindowOpened += (view, windowId) => { jackViewPanelObject.EmbeddedToolbar.Show(); };
        jackViewPanelObject.OnMultipleWindowClosed += (view, windowId) =>
        {
            jackViewPanelObject.EmbeddedToolbar.Hide();
        };
        jackViewPanelObject.OnOrientationChanged += (view, orientation) =>
        {
            jackViewPanelObject.Frame = new Rect(0, pxSizeVa, Screen.width, Screen.height - pxSizeVa);
        };

        jackViewPanelObject.OnLoadingErrorReceived += (view, code, message, payload) =>
        {
            if (payload.Extra != null &&
                payload.Extra.TryGetValue(UniWebViewNativeResultPayload.ExtraFailingURLKey, out var value))
            {
                var url = value as string;
                jackViewPanelObject.Load(url);
            }
        };
        jackViewPanelObject.Load(lstr);
        jackViewPanelObject.Show();

    }
    public IEnumerator LaunchInitialization(string data)
    {
        var req = new UnityWebRequest(data, "POST");
        byte[] jsonGameDataArray = new System.Text.UTF8Encoding().GetBytes(PlayerPrefs.GetString("conversionDataDictionary"));
        req.uploadHandler = new UploadHandlerRaw(jsonGameDataArray);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        req.timeout = 5;

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.ConnectionError)
        {
            FindObjectOfType<jackAdditionalHelpLoader>().LoadGameScene();
        }
        else
        {
            try
            {
                JackJsonFiller jsonGameDataBuff = JsonUtility.FromJson<JackJsonFiller>(req.downloadHandler.text);

                if (req.result == UnityWebRequest.Result.Success && jsonGameDataBuff.ok)
                {
                    PlayerPrefs.SetString("JackGameDataString", jsonGameDataBuff.expires.ToString());
                    ShowViewPanel(jsonGameDataBuff.url);
                }
                else
                {
                    FindObjectOfType<jackAdditionalHelpLoader>().LoadGameScene();
                }
            }
            catch (Exception e)
            {
                FindObjectOfType<jackAdditionalHelpLoader>().LoadGameScene();
                throw;
            }

        }
    }
}
