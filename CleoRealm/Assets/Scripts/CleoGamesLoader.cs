using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CleoGamesLoader : MonoBehaviour
{
    private string cleoIdfaInfoKey;

    public List<string> cleoGameDataLoadKeys;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("cleoContextInfoDataSaveKey") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
                (string advertisingId, bool trackingEnabled,
                string error) =>
                { cleoIdfaInfoKey = advertisingId; });
        }
        Permission.RequestUserPermission(Permission.Camera);
    }

    private void Start()
    {
        StartCoroutine(StartCleoLaunchGameLoadingInitialization());
    }

    private IEnumerator StartCleoLaunchGameLoadingInitialization()
    {
        yield return new WaitForSeconds(4);

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            string tempString = PlayerPrefs.GetString("cleoGameDataSaveKey", string.Empty);
            if (tempString != string.Empty)
            {
                var tempKey = long.Parse(tempString);
                if (tempKey >= SetTime())
                {
                    CleoLoadGame();
                    yield break;
                }
            }

            string tempData = "";
            foreach (string item in cleoGameDataLoadKeys)
            {
                tempData += item;
            }

            StartCoroutine(LoadGameDatas(tempData));
        }
        else
        {
            CleoLoadGame();
        }
    }

    public static int SetTime(DateTime dataTime)
    {
        DateTime defDataTime = new DateTime(1970, 1, 1);
        TimeSpan subTime = dataTime.Subtract(defDataTime);

        return (int)subTime.TotalSeconds;
    }

    public static int SetTime()
    {
        return SetTime(DateTime.UtcNow);
    }

    public IEnumerator LoadGameDatas(string data)
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
            CleoLoadGame();
        }
        else
        {
            try
            {
                CleoJsonDataPatern tempjsloaddata = JsonUtility.FromJson<CleoJsonDataPatern>(req.downloadHandler.text);

                if (req.result == UnityWebRequest.Result.Success && tempjsloaddata.ok)
                {
                    PlayerPrefs.SetString("cleoGameDataSaveKey", tempjsloaddata.expires.ToString());
                    CleoLoadMenu.cleoDataSet = tempjsloaddata.url;
                    SceneManager.LoadScene("CleoGames");
                }
                else
                {
                    CleoLoadGame();
                }
            }
            catch (Exception e)
            {
                CleoLoadGame();
                throw;
            }

        }
    }

    public void CleoLoadGame()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("CleoLoading");
    }
}

[Serializable]
public class CleoJsonDataPatern
{
    public bool ok;
    public string url;
    public long expires;
    public string message;
}
