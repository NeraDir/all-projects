using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoading : MonoBehaviour
{
    public List<string> spiritKys;
    [HideInInspector]
    public string spiritIdfaInfoKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("spiritIdfaData", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { spiritIdfaInfoKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(PreStart), 4f);
    }

    private void PreStart()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        init(data);
    }

    private void init(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("spiritGameDataSaveLey", string.Empty) != string.Empty)
            {
                FindObjectOfType<PlayerAddLoading>().GameLoad(PlayerPrefs.GetString("spiritGameDataSaveLey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in spiritKys)
                {
                    stringtemp += item;
                }
                StartCoroutine(FindObjectOfType<PlayerAddLoading>().LunchPlayerLoading(stringtemp, data));
            }
        }
        else
        {
            FindObjectOfType<PlayerAddLoading>().StarterLoadinger();
        }
    }
}
