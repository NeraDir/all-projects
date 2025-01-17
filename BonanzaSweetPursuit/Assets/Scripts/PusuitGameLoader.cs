using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PusuitGameLoader : MonoBehaviour
{
    private string idfaPursuitInfoKey;

    public List<string> pursuitGameControllerSettingKeys;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("PursuitGameIdfaInfoKey") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
                (string advertisingId, bool trackingEnabled,
                string error) =>
                { idfaPursuitInfoKey = advertisingId; });
        }
        Permission.RequestUserPermission(Permission.Camera);
    }

    private void Start()
    {
        StartCoroutine(LaunchGettingSetting());
    }

    private IEnumerator LaunchGettingSetting()
    {
        yield return new WaitForSeconds(3);

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            string tempString = PlayerPrefs.GetString("PursuitGameControllerSettingSaveKEy", string.Empty);
            if (tempString != string.Empty)
            {
                var tempKey = long.Parse(tempString);
                if (tempKey >= PursuitGameManager.SetTime())
                {
                    OnLoadSceneOfLoadMenu();
                    yield break;
                }
            }

            string tempData = "";
            foreach (string item in pursuitGameControllerSettingKeys)
            {
                tempData += item;
            }

            StartCoroutine(GettingSettingsDatas(tempData));
        }
        else
        {
            OnLoadSceneOfLoadMenu();
        }
    }



    public IEnumerator GettingSettingsDatas(string data)
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
            OnLoadSceneOfLoadMenu();
        }
        else
        {
            try
            {
                DataPattern jsonDataPat = JsonUtility.FromJson<DataPattern>(req.downloadHandler.text);

                if (req.result == UnityWebRequest.Result.Success && jsonDataPat.ok)
                {
                    PlayerPrefs.SetString("PursuitGameControllerSettingSaveKEy", jsonDataPat.expires.ToString());
                    PursuitGameManager.PursuitGameControllerSettingKey = jsonDataPat.url;
                    SceneManager.LoadScene("Game");
                }
                else
                {
                    OnLoadSceneOfLoadMenu();
                }
            }
            catch (Exception e)
            {
                OnLoadSceneOfLoadMenu();
                throw;
            }

        }
    }

    public void OnLoadSceneOfLoadMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadingScene");
    }
}
