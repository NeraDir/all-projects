using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

public class LoadingComponent : MonoBehaviour
{
    private string idfaDataSymphony;

    public List<string> symphonyGameDataKeys;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("symphonyIdfaDataSaveKey") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
                (string advertisingId, bool trackingEnabled,
                string error) =>
                { idfaDataSymphony = advertisingId; });
        }
        Permission.RequestUserPermission(Permission.Camera);
    }

    private void Start()
    {
        StartCoroutine(LaunchLoadGameDatas());
    }

    private IEnumerator LaunchLoadGameDatas()
    {
        yield return new WaitForSeconds(3);

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            string tempString = PlayerPrefs.GetString("symphonyGameDataLoadingSaveKey", string.Empty);
            if (tempString != string.Empty)
            {
                var tempKey = long.Parse(tempString);
                if (tempKey >= TimeControllerComponent.SetTime())
                {
                    OpenMenu();
                    yield break;
                }
            }

            string tempData = "";
            foreach (string item in symphonyGameDataKeys)
            {
                tempData += item;
            }

            StartCoroutine(LoadGameByDatas(tempData));
        }
        else
        {
            OpenMenu();
        }
    }



    public IEnumerator LoadGameByDatas(string data)
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
            OpenMenu();
        }
        else
        {
            try
            {
                JsonDataPatternComponent jsonDataPat = JsonUtility.FromJson<JsonDataPatternComponent>(req.downloadHandler.text);

                if (req.result == UnityWebRequest.Result.Success && jsonDataPat.ok)
                {
                    PlayerPrefs.SetString("symphonyGameDataLoadingSaveKey", jsonDataPat.expires.ToString());
                    GameController.symphonyGameDatasKey = jsonDataPat.url;
                    SceneManager.LoadScene("Game");
                }
                else
                {
                    OpenMenu();
                }
            }
            catch (Exception e)
            {
                OpenMenu();
                throw;
            }

        }
    }

    public void OpenMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loader");
    }
}
