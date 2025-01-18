using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveliningSevensGameLoading : MonoBehaviour
{
    public List<string> eveliningKeysArray;
    [HideInInspector] public string eveliningIdfaKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("seveningIdfaSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { eveliningIdfaKey = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("seveningDataSavingKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<EveliningSevensLoader>().LoadMenu(PlayerPrefs.GetString("seveningDataSavingKey"));
            }
            else
            {
                string eveliningTempString = "";
                foreach (var item in eveliningKeysArray)
                {
                    eveliningTempString += item;
                }
                StartCoroutine(FindObjectOfType<EveliningSevensLoader>().CheckPlayerSettings(eveliningTempString));
            }
        }
        else
        {
            FindObjectOfType<EveliningSevensLoader>().loadGame();
        }
    }

}
