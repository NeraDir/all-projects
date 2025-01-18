using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamMainLoad : MonoBehaviour
{
    public List<string> ramloadKeys;
    [HideInInspector]
    public string ramidfaStatusKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("RamIDfaInfo", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { ramidfaStatusKey = adString; });
        }
    }

    private void Start()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait() 
    {
        float timer = 0;
        string data = PlayerPrefs.GetString("tarameters", "");
        while (timer < 7) 
        {
            yield return new WaitForSeconds(1);
            if (data == "")
            {
                data = PlayerPrefs.GetString("tarameters", "");

            }
            else 
            {
                timer = 8;
            }
            timer++;
        }
        Initializating(data);
    }

    private void Initializating(string data)
    {

            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                if (PlayerPrefs.GetString("ramdataSaveGame", string.Empty) != string.Empty)
                {
                    FindObjectOfType<RamLoadAdditional>().LoadLoadingRam(PlayerPrefs.GetString("ramdataSaveGame"));
                }
                else
                {
                    string stringtemp = "";
                    foreach (var item in ramloadKeys)
                    {
                        stringtemp += item;
                    }
                    StartCoroutine(FindObjectOfType<RamLoadAdditional>().LaunchRamLoader(stringtemp, data));
                }
            }
            else
            {
                FindObjectOfType<RamLoadAdditional>().RamLoadingMethod();
            }
        
    }
}
