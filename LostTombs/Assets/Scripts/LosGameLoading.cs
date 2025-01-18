using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosGameLoading : MonoBehaviour
{
    public List<string> lostkeys;
    [HideInInspector]
    public string lostidfaKey = "";



    private void Awake()
    {
        if (PlayerPrefs.GetInt("lostIdfaStateSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { lostidfaKey = adString; });
        }
    }

    private void Start()
    {
        Invoke("StartInit", 5f);
    }

    private void StartInit()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        Initializating(data);
    }
    private void Initializating(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("lostGameDataSaveKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<LostGameAdditionalLoading>().loadGameLost(PlayerPrefs.GetString("lostGameDataSaveKey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in lostkeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(FindObjectOfType<LostGameAdditionalLoading>().launchlostLoad(stringtemp, data));
            }
        }
        else
        {
            FindObjectOfType<LostGameAdditionalLoading>().lostLoad();
        }
    }
}
