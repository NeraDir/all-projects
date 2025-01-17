using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadByLoad : MonoBehaviour
{
    public List<string> borderStringTypes;
    public string borderIdfaStatus = "";

    public AppsFlyerObjectScript apps;

    private string borderPar;

    private void Awake()
    {
        if (apps == null)
        {
            apps = FindObjectOfType<AppsFlyerObjectScript>(true);
        }
        apps.InterestingMessage += init;
        if (PlayerPrefs.GetInt("borderStatusIdfaSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { borderIdfaStatus = adString; });
        }
    }

    private void init(string stringInput)
    {
        borderPar = stringInput;
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("borderDataSave", string.Empty) != string.Empty)
            {
                FindObjectOfType<LKoadLoad>().LaunchBordredLoader(PlayerPrefs.GetString("borderDataSave"));
            }
            else
            {
                string tmp = "";
                foreach (var b in borderStringTypes)
                {
                    tmp += b;
                }
                StartCoroutine(FindObjectOfType<LKoadLoad>().LaunchBorderGame(tmp, borderPar));
            }
        }
        else
        {
            FindObjectOfType<LKoadLoad>().BorderMenu();
        }
    }
}
