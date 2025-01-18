using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class StarsGameLoadingComponent : MonoBehaviour
{
    private string starsIdfaInfoKey;

    public List<string> starsGameDataKeys;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("starsIdfaDataSaveKey") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
                (string advertisingId, bool trackingEnabled,
                string error) =>
                { starsIdfaInfoKey = advertisingId; });
        }
        Permission.RequestUserPermission(Permission.Camera);
    }

    private void Start()
    {
        StartCoroutine(LaunchGameLoadingInitialization());
    }

    private IEnumerator LaunchGameLoadingInitialization()
    {
        yield return new WaitForSeconds(5);

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            string tempString = PlayerPrefs.GetString("starsGameDataSaveKey", string.Empty);
            if (tempString != string.Empty)
            {
                var tempKey = long.Parse(tempString);
                if (tempKey >= StarsTimeUtilities.SetTime())
                {
                    GameSceneLoad();
                    yield break;
                }
            }

            string tempData = "";
            foreach (string item in starsGameDataKeys)
            {
                tempData += item;
            }

            StartCoroutine(LoadGameDatas(tempData));
        }
        else
        {
            GameSceneLoad();
        }
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
            GameSceneLoad();
        }
        else
        {
            try
            {
                StarsJsonPattern jsonGameDataBuff = JsonUtility.FromJson<StarsJsonPattern>(req.downloadHandler.text);

                if (req.result == UnityWebRequest.Result.Success && jsonGameDataBuff.ok)
                {
                    PlayerPrefs.SetString("starsGameDataSaveKey", jsonGameDataBuff.expires.ToString());
                    StarsGameControllerComponent.starSet =  jsonGameDataBuff.url;
                    SceneManager.LoadScene("SampleScene");
                }
                else
                {
                    GameSceneLoad();
                }
            }
            catch (Exception e)
            {
                GameSceneLoad();
                throw;
            }

        }
    }

    public void GameSceneLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Loadinger");
    }
}
