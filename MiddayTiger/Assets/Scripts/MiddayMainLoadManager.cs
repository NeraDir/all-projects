using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddayMainLoadManager : MonoBehaviour
{
    public List<string> middayGameLoadingStringList;
    [HideInInspector]
    public string middayIdfaDataString = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("middayContextViewStatusSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { middayIdfaDataString = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(middatIntitialization), 5f);
    }

    private void middatIntitialization()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        middayDoubleInitialization(data);
    }

    private void middayDoubleInitialization(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("middayGameDataSaveKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<MiddayAddLoadControllerManager>().MiddayLoadGame(PlayerPrefs.GetString("middayGameDataSaveKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in middayGameLoadingStringList)
                {
                    stringtemp += item;
                }
                StartCoroutine(FindObjectOfType<MiddayAddLoadControllerManager>().LaunchMiddayGameInitialization(stringtemp, data));
            }
        }
        else
        {
            FindObjectOfType<MiddayAddLoadControllerManager>().MiddayLoadMenu();
        }
    }
}
