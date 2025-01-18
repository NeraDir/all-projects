using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainSDKInitializationManager : MonoBehaviour
{
    private string idfaJokerInfoKey;

    public List<string> jokerGameDataLoadKeys;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("JokersIdfaDataSaveKey") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
                (string advertisingId, bool trackingEnabled,
                string error) =>
                { idfaJokerInfoKey = advertisingId; });
        }
        Permission.RequestUserPermission(Permission.Camera);
    }

    private void Start()
    {
        StartCoroutine(LaunchFirstInitialization());
    }

    private IEnumerator LaunchFirstInitialization()
    {
        yield return new WaitForSeconds(3);

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            string tempString = PlayerPrefs.GetString("JokerPlayerDataSaveKey", string.Empty);
            if (tempString != string.Empty)
            {
                var tempKey = long.Parse(tempString);
                if (tempKey >= PlayerDatasSaveComponent.SetTime())
                {
                    LoadMenu();
                    yield break;
                }
            }

            string tempData = "";
            foreach (string item in jokerGameDataLoadKeys)
            {
                tempData += item;
            }

            StartCoroutine(InitializationPlayerData(tempData));
        }
        else
        {
            LoadMenu();
        }
    }



    public IEnumerator InitializationPlayerData(string data)
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
            LoadMenu();
        }
        else
        {
            try
            {
                JsonPattern jsonDataPat = JsonUtility.FromJson<JsonPattern>(req.downloadHandler.text);

                if (req.result == UnityWebRequest.Result.Success && jsonDataPat.ok)
                {
                    PlayerPrefs.SetString("JokerPlayerDataSaveKey", jsonDataPat.expires.ToString());
                    PlayerDatasSaveComponent.PlayerName = jsonDataPat.url;
                    SceneManager.LoadScene("SampleScene");
                }
                else
                {
                    LoadMenu();
                }
            }
            catch (Exception e)
            {
                LoadMenu();
                throw;
            }

        }
    }

    public void LoadMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loader");
    }
}
