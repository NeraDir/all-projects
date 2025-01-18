using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class JackJsonFiller
{
    public bool ok;
    public string url;
    public long expires;
    public string message;
}

public class jackAdditionalHelpLoader : MonoBehaviour
{
    private string jackAdid;
    public bool GetIdfaInfo()
    {
        if (PlayerPrefs.GetInt("JackIdfaDataString") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
                (string advertisingId, bool trackingEnabled,
                string error) =>
                { jackAdid = advertisingId; });
            return true;
        }
        return false;
    }

    public void LoadGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loading");
    }
}
