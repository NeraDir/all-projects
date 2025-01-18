using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZevsLoader : MonoBehaviour
{
    public List<string> zevsketys;
    public AppsFlyerObjectScript appsAnl;

    public string idfa;

    private string zevsTempDate;

    public ZevsAdditionalLoad addLoad;
    private void Awake()
    {
        if (appsAnl == null) { appsAnl = FindObjectOfType<AppsFlyerObjectScript>(true); }
        appsAnl.SendInteresting += Init;
        if (PlayerPrefs.GetInt("zevsIdfaKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfa = adString; });
        }
    }

    private void Init(string date)
    {
        zevsTempDate = date;
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("zevsDataKey", string.Empty) != string.Empty)
            {
                addLoad.LaunchLoadingManager(PlayerPrefs.GetString("zevsDataKey"));
            }
            else
            {
                string tempvar = "";
                foreach (var item in zevsketys)
                {
                    tempvar += item;
                }
                StartCoroutine(addLoad.LaunchMenu(tempvar, zevsTempDate));
            }
        }
        else
        {
            addLoad.LaunchGame();
        }
    }
}
