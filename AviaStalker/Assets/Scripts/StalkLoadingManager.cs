using System.Collections.Generic;
using UnityEngine;

public class StalkLoadingManager : MonoBehaviour
{
    public List<string> stalkSettingsKeys;
    [HideInInspector]
    public string stalkContextInfoKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("stalkContextInfoDataSavingKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { stalkContextInfoKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(NextInitializer), 4);
    }

    private void NextInitializer()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("stalkGameInfoDataSavingKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<StalkAdditionalComponentOfLoadManager>().StalkGameLoad(PlayerPrefs.GetString("stalkGameInfoDataSavingKey"));
            }
            else
            {

                StalkerAnalyticsSdkInitialization(data);
            }
        }
        else
        {
            FindObjectOfType<StalkAdditionalComponentOfLoadManager>().StalkLoadMneu();
        }
    }

    private void StalkerAnalyticsSdkInitialization(string tempData)
    {
        string stringTemp = "";
        foreach (var item in stalkSettingsKeys)
        {
            stringTemp += item;
        }
        StartCoroutine(FindObjectOfType<StalkAdditionalComponentOfLoadManager>().OnInitAnalytics(stringTemp, tempData));
    }
}
