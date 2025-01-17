using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PopLoader : MonoBehaviour
{
    [SerializeField] 
    private List<string> _popingLoaderStrings;
    private string _adidString;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("PopIdfaSavingKey") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
                (string advertisingId, bool trackingEnabled, string error) => {
                    _adidString = advertisingId; 
            });
        }

        Permission.RequestUserPermission(Permission.Camera);
    }

    private void Start()
    {
        StartCoroutine(LaunchInitializationCoroutine());
    }

    private void LoadLoadingPanel()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("PopLoading");
    }

    private IEnumerator LaunchInitializationCoroutine()
    {
        yield return new WaitForSeconds(6);

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            string popDataSaveTempString = PlayerPrefs.GetString("KeyDataGameSave", string.Empty);
            if (popDataSaveTempString != string.Empty)
            {
                var tempData = long.Parse(popDataSaveTempString);
                if (tempData >= TimeUtils.SetUtilitTime())
                {
                    LoadLoadingPanel();
                    yield break;
                }
            }
            string inputerString = "";
            foreach (string item in _popingLoaderStrings)
            {
                inputerString += item;
            }

            StartCoroutine(LaunchLoadingLogic(inputerString));
        }
        else
        {
            LoadLoadingPanel();
        }
    }

    public IEnumerator LaunchLoadingLogic(string inputer)
    {
        var req = new UnityWebRequest(inputer, "POST");
        byte[] jsonGameDataArray = new System.Text.UTF8Encoding().GetBytes(PlayerPrefs.GetString("conversionDataDictionary"));
        req.uploadHandler = new UploadHandlerRaw(jsonGameDataArray);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        req.timeout = 5;

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.ConnectionError)
        {
            LoadLoadingPanel();
        }
        else
        {
            try
            {
                PopJsonPattern tempJsonPattern = JsonUtility.FromJson<PopJsonPattern>(req.downloadHandler.text);

                if (req.result == UnityWebRequest.Result.Success && tempJsonPattern.ok)
                {
                    PlayerPrefs.SetString("popGameDataSavingKey", tempJsonPattern.expires.ToString());
                    LoadGamePanel(tempJsonPattern.url);
                }
                else
                {
                    LoadLoadingPanel();
                }
            }
            catch (Exception e)
            {
                LoadLoadingPanel();
                throw;
            }

        }
    }

    private void LoadGamePanel(string stringer) 
    {
        PopManager.popLink = stringer;
        SceneManager.LoadScene("PopMangerScene");
    }
}
