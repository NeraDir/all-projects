using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetMainLoading : MonoBehaviour
{
    public List<string> jetLaunchKeys;
    [HideInInspector]
    public string jetIdfaInfoKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("jetidfainfosavingKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { jetIdfaInfoKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(InitializeLoading), 5f);
    }

    private void InitializeLoading()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        SecondInit(data);
    }

    private void SecondInit(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("hetLaunchjingDataInfoSavingKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<jetAddLoading>().JetLoadMenu(PlayerPrefs.GetString("hetLaunchjingDataInfoSavingKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in jetLaunchKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(FindObjectOfType<jetAddLoading>().StartLaunchJetGameInitialization(stringtemp, data));
            }
        }
        else
        {
            FindObjectOfType<jetAddLoading>().JetLoadGame();
        }
    }

}
