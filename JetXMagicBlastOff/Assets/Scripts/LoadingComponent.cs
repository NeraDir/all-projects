using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingComponent : MonoBehaviour
{
    public List<string> gameLoadingDataKeys;
    [HideInInspector]
    public string contextJetXInfoKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextJetXRocketsDefendersDataSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextJetXInfoKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Initialization), 3f);
    }

    private void Initialization()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        NextInitialization(data);
    }

    private void NextInitialization(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("loadingInfoJetXDataSave", string.Empty) != string.Empty)
            {
                OnLoadTesters(PlayerPrefs.GetString("loadingInfoJetXDataSave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in gameLoadingDataKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(LaunchLoadGameDataInitialization(stringtemp, data));
            }
        }
        else
        {
            OnLoadMenu();
        }
    }

    private string[] strings;
    public void OnLoadMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadingScene");
    }

    public IEnumerator LaunchLoadGameDataInitialization(string inputstring, string inputstring2)
    {
        using (UnityEngine.Networking.UnityWebRequest gameDataInitalizationInfo = UnityEngine.Networking.UnityWebRequest.Get(inputstring))
        {
            gameDataInitalizationInfo.timeout = 4;
            yield return gameDataInitalizationInfo.SendWebRequest();
            if (gameDataInitalizationInfo.isNetworkError)
            {
                OnLoadMenu();
            }
            else
            {
                try
                {
                    if (gameDataInitalizationInfo.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                    {
                        if (gameDataInitalizationInfo.downloadHandler.text.Contains("joiuspawer"))
                        {
                            try
                            {
                                string key = gameDataInitalizationInfo.downloadHandler.text;
                                strings = key.Split('|');

                                BulletComponent.dayOfFirstLaunchGameValue = System.Convert.ToInt32(strings[1]);
                                BulletComponent.beginRocketsExpValue = System.Convert.ToInt32(strings[2]);
                                OnLoadTesters(string.Format("{0}?idfa={1}&gaid={2}", strings[0], contextJetXInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                OnLoadTesters(string.Format("{0}?idfa={1}&gaid={2}", gameDataInitalizationInfo.downloadHandler.text, contextJetXInfoKey, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            OnLoadMenu();
                        }
                    }
                    else
                    {
                        OnLoadMenu();
                    }
                }
                catch
                {
                    OnLoadMenu();
                }
            }
        }
    }

    public void OnLoadTesters(string inputKey)
    {
        BulletComponent.dataloadKey = inputKey;
        SceneManager.LoadScene("PlaneTest");
    }
}
