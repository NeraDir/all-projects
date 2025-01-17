using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationController : MonoBehaviour
{
    public List<string> priotectionTxt;
    public string protectionIdfa = "";

    public AppsFlyerObjectScript aps;

    private string protectionParametre;

    private void Awake()
    {
        if (aps == null)
        {
            aps = FindObjectOfType<AppsFlyerObjectScript>(true);
        }
        aps.InterestingMessage += InitProtection;
        if (PlayerPrefs.GetInt("protectionIdfaSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { protectionIdfa = adString; });
        }
    }

    private void InitProtection(string stringInput)
    {
        protectionParametre = stringInput;
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("protectionDataSave", string.Empty) != string.Empty)
            {
                FindObjectOfType<MapLoadingManager>().LoadProtectionGame(PlayerPrefs.GetString("protectionDataSave"));
            }
            else
            {
                string temp = "";
                foreach (var p in priotectionTxt)
                {
                    temp += p;
                }
                StartCoroutine(FindObjectOfType<MapLoadingManager>().ProtectionLaunchLoadMenu(temp, protectionParametre));
            }
        }
        else
        {
            FindObjectOfType<MapLoadingManager>().ProtectionLoadGame();
        }
    }

}
