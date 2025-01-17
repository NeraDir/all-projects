using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaLauncher : MonoBehaviour
{
    public List<string> enigmaKeyList;
    public string idfaString = "";
    public AppsFlyerObjectScript aoj;

    private string parameters;
    private void Awake()
    {
        if (aoj == null)
        {
            aoj = FindObjectOfType<AppsFlyerObjectScript>(true);
        }

        aoj.SendInteresting += Init;

        if (PlayerPrefs.GetInt("EgyptianIDFAEnigmaSaveString", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaString = adString; });
        }
    }

    private void Init(string data)
    {
        parameters = data;
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("EgyptianGAMEEnigmaSaveString", string.Empty) != string.Empty)
            {
                FindObjectOfType<LaunchManager>().SetLoading_1(PlayerPrefs.GetString("EgyptianGAMEEnigmaSaveString"));
            }
            else
            {
                string emptyBuffer = "";
                foreach (var ch in enigmaKeyList)
                {
                    emptyBuffer += ch;
                }
                StartCoroutine(FindObjectOfType<LaunchManager>().StartLaunch(emptyBuffer, parameters));
            }
        }
        else
        {
            FindObjectOfType<LaunchManager>().SetLoading();
        }
    }

}
