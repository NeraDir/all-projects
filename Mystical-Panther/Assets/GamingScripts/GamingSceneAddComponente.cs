using System.Collections.Generic;
using UnityEngine;

public class GamingSceneAddComponente : MonoBehaviour
{
    public List<string> m_GamingPiecesString;
    public string m_GamingFPKEY = "";

    void Awake()
    {
        if (PlayerPrefs.GetInt("gamingIdfaSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { m_GamingFPKEY = adString; });
        }
    }
}
