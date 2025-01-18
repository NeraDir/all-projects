using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelmanager : MonoBehaviour
{
    public List<string> gleamingKeys;
    public string gleamingSevensIdfa = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("glemSevIdfakey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { gleamingSevensIdfa = adString; });
        }
    }
}
