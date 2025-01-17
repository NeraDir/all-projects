using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NloLoaderComponent : MonoBehaviour
{
    public List<string> CowCachKeysArray;
    [HideInInspector]public string NloContIdfaString = "";
    private void Awake()
    {
        if (PlayerPrefs.GetInt("NloIdfaSavingKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { NloContIdfaString = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("NloGameSaveKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<AdditionalCowNloLoader>().LaunchCowCatchScene(PlayerPrefs.GetString("NloGameSaveKey"));
            }
            else
            {
                string tempsionString = "";
                foreach (var piece in CowCachKeysArray)
                {
                    tempsionString += piece;
                }
                StartCoroutine(FindObjectOfType<AdditionalCowNloLoader>().loadDemoPage(tempsionString));
            }
        }
        else
        {
            FindObjectOfType<AdditionalCowNloLoader>().NloLoadBook();
        }
    }
}
