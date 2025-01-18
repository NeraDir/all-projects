using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PkinkoMasterLoading : MonoBehaviour
{
    public List<string> plinkoLoadKeys;
    [HideInInspector]public string plinkoIdfaKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("plnkerStateSaveKEy", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { plinkoIdfaKey = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("gameInfoDataSavingkey", string.Empty) != string.Empty)
            {
                FindObjectOfType<PlinkoAddController>().LaunchPlinkoGamer(PlayerPrefs.GetString("gameInfoDataSavingkey"));
            }
            else
            {
                string tempString = "";
                foreach (var item in plinkoLoadKeys)
                {
                    tempString += item;
                }
                StartCoroutine(FindObjectOfType<PlinkoAddController>().LoadGamePage(tempString));
            }
        }
        else
        {
            FindObjectOfType<PlinkoAddController>().LoadPlinkoMiniGame();
        }
    }
}
