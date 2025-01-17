using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializationmanager : MonoBehaviour
{
    public List<string> fairyPiecesList;
    public string fairyFpoTempKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("fairyMasterIDFAkey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { fairyFpoTempKey = adString; });
        }
    }
}
