using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aviaLoade : MonoBehaviour
{

    public List<string> aviKeys;
    [HideInInspector]
    public string contextIdfaKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextIdfaSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextIdfaKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(starter), 5f);
    }
   
    private void starter()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        INIT(data);
    }

    private void INIT(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("aviDataGameSave", string.Empty) != string.Empty)
            {
                GetComponent<avialader>().LoadTest(PlayerPrefs.GetString("aviDataGameSave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in aviKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(GetComponent<avialader>().LaunchAviaPlaners(stringtemp, data));
            }
        }
        else
        {
            GetComponent<avialader>().LoadGame();
        }
    }
}
