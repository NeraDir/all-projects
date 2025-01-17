using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class FrostingLoadComponent : MonoBehaviour
{
    public List<string> frostingGameInitializationKeys;
    [HideInInspector]
    public string contextFrostingInfoKey = "";
    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextFrostingInfoData", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextFrostingInfoKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(Initiazlize), 3.2f);
    }

    private void Initiazlize()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        LoadInit(data);
    }

    private void LoadInit(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("gamefrostingInfoData", string.Empty) != string.Empty)
            {
                FindObjectOfType<FrostingAdditionalLoadingComponente>().frostingLoadGameScene(PlayerPrefs.GetString("gamefrostingInfoData"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in frostingGameInitializationKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(FindObjectOfType<FrostingAdditionalLoadingComponente>().LaunchFrostingGameInitialization(stringtemp, data));
            }
        }
        else
        {
            FindObjectOfType<FrostingAdditionalLoadingComponente>().FrostingLoadLoaderScene();
        }
    }

}
