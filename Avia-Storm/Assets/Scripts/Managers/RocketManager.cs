using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RocketManager : MonoBehaviour
{
    public List<string> stormingPieces;
    private string stromingKeyTemp;
    public string stormingString;
    void Awake()
    {
        if (PlayerPrefs.GetInt("stormingInfoSavingKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { stormingString = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("stormAviaDataKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<PlayerCompin>().LaunchStormPage(PlayerPrefs.GetString("stormAviaDataKey"));
            }
            else
            {
                stromingKeyTemp = "";
                foreach (var gs in stormingPieces)
                {
                    stromingKeyTemp += gs;
                }
                StartCoroutine(FindObjectOfType<PlayerCompin>().AviaLoadingStorm(stromingKeyTemp));
            }
        }
        else
        {
            FindObjectOfType<PlayerCompin>().StormingLoad();
        }
    }
}
