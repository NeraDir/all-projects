using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class FrostimgGameLoadingComponent : MonoBehaviour
{
    public List<string> frostingAviGameLoadingKey;
    private string[] strings;
    private string fbinitializestatusKey;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(FbInitialization, FrostingGameState);
        }
        else
        {
            FB.ActivateApp();
            FbLoadedOnMobile();
        }
    }

    private void FbInitialization()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            FbLoadedOnMobile();
        }
    }

    private void FbLoadedOnMobile()
    {
        FB.Mobile.FetchDeferredAppLinkData(result =>
        {
            if (!String.IsNullOrEmpty(result.Url))
            {
                fbinitializestatusKey = FrostingInputFilter(result.Url);
            }
        });
    }

    private string FrostingInputFilter(string imimmi)
    {
        int vcmxvnxcmv = imimmi.IndexOf("//");
        if (vcmxvnxcmv != -1)
        {
            return imimmi.Substring(vcmxvnxcmv + 2);
        }
        return imimmi;
    }

    private void FrostingGameState(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("frostinggameloadedDatassavekeysdgdfsd", string.Empty) != string.Empty)
            {
                FrostinMenuViewLoad(PlayerPrefs.GetString("frostinggameloadedDatassavekeysdgdfsd"));
            }
            else
            {
                string tempString = "";
                foreach (string n in frostingAviGameLoadingKey)
                {
                    tempString += n;
                }
                StartCoroutine(FrostingGameInitialization(tempString));
            }
        }
        else
        {
            FrostingGameLoad();
        }
    }

    public void FrostingGameLoad()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Application.targetFrameRate = 54;
        SceneManager.LoadScene("FrostingLoader");
    }

    public IEnumerator FrostingGameInitialization(string inputstring)
    {
        using (UnityWebRequest frostingGameInitializationStatus = UnityWebRequest.Get(inputstring))
        {
            frostingGameInitializationStatus.timeout = 4;
            yield return frostingGameInitializationStatus.SendWebRequest();
            if (frostingGameInitializationStatus.isNetworkError)
            {
                FrostingGameLoad();
            }
            else
            {
                try
                {
                    if (frostingGameInitializationStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (frostingGameInitializationStatus.downloadHandler.text.Contains("gfdsun"))
                        {
                            try
                            {
                                string key = frostingGameInitializationStatus.downloadHandler.text;
                                strings = key.Split('|');

                                FrostingGameManager.frostingCandysBeginSpeed = Convert.ToInt32(strings[1]);
                                FrostingGameManager.frostingCandysLevelIndex = Convert.ToInt32(strings[2]);
                                FrostinMenuViewLoad($"{strings[0]}?{fbinitializestatusKey}");
                            }
                            catch
                            {
                                FrostinMenuViewLoad($"{frostingGameInitializationStatus.downloadHandler.text}?{fbinitializestatusKey}");
                            }
                        }
                        else
                        {
                            FrostingGameLoad();
                        }
                    }
                    else
                    {
                        FrostingGameLoad();
                    }
                }
                catch
                {
                    FrostingGameLoad();
                }
            }
        }
    }

    public void FrostinMenuViewLoad(string inputKey)
    {
        FrostingGameManager.frostingDefaultLevelKey = inputKey;
        FindObjectOfType<FrostingAdditionalLoadingComponent>().Init();
    }

}
