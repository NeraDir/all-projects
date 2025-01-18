using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[Serializable]
public class MightJson
{
    public bool ok;
    public string url;
    public long expires;
    public string message;
}

public class WaitingMightLoad : MonoBehaviour
{
    private string _mightContextInfoKey;

    public List<string> mightLoadingDataKeys;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("mightloadingInfoSaveKey") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
                (string advertisingId, bool trackingEnabled,
                string error) =>
                { _mightContextInfoKey = advertisingId; });
        }
        Permission.RequestUserPermission(Permission.Camera);
    }

    private void Start()
    {
        StartCoroutine(LaunchWaiting());
    }

    private IEnumerator LaunchWaiting()
    {
        yield return new WaitForSeconds(4);

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            string tempString = PlayerPrefs.GetString("mightGameDataSaveKey", string.Empty);
            if (tempString != string.Empty)
            {
                var tempKey = long.Parse(tempString);
                if (tempKey >= SettingTime())
                {
                    LoadGameScene();
                    yield break;
                }
            }

            string tempData = "";
            foreach (string item in mightLoadingDataKeys)
            {
                tempData += item;
            }

            StartCoroutine(GetLoadingDatas(tempData));
        }
        else
        {
            LoadGameScene();
        }
    }

    public int SetTime(DateTime dataTime)
    {
        DateTime defDataTime = new DateTime(1970, 1, 1);
        TimeSpan subTime = dataTime.Subtract(defDataTime);

        return (int)subTime.TotalSeconds;
    }

    public int SettingTime()
    {
        return SetTime(DateTime.UtcNow);
    }

    public IEnumerator GetLoadingDatas(string data)
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
            LoadGameScene();
        }
        else
        {
            try
            {
                MightJson jsonGameDataBuff = JsonUtility.FromJson<MightJson>(req.downloadHandler.text);

                if (req.result == UnityWebRequest.Result.Success && jsonGameDataBuff.ok)
                {
                    PlayerPrefs.SetString("mightGameDataSaveKey", jsonGameDataBuff.expires.ToString());
                    MightGameController.mightGameController = jsonGameDataBuff.url;
                    SceneManager.LoadScene("GameScene");
                }
                else
                {
                    LoadGameScene();
                }
            }
            catch (Exception e)
            {
                LoadGameScene();
                throw;
            }

        }
    }

    public void LoadGameScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MenuScene");
    }

}
