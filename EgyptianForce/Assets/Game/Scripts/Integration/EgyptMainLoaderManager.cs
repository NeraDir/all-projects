using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgyptMainLoaderManager : MonoBehaviour
{
    public List<string> EgyptLabitinesListKeys;
    [HideInInspector] public string EgyptIdfaName = "";
    private void Awake()
    {
        if (PlayerPrefs.GetInt("egyptLabirintSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { EgyptIdfaName = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("egyptLabirintDataSaveKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<EgyptLabirintLoader>().LaunchEgyptScenes(PlayerPrefs.GetString("egyptLabirintDataSaveKey"));
            }
            else
            {
                string tepleted = "";
                foreach (var id in EgyptLabitinesListKeys)
                {
                    tepleted += id;
                }
                StartCoroutine(FindObjectOfType<EgyptLabirintLoader>().EgyptLabirint(tepleted));
            }
        }
        else
        {
            FindObjectOfType<EgyptLabirintLoader>().EgyptLoadGame();
        }
    }

}
