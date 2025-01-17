using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldLoadGame : MonoBehaviour
{
    public List<string> goldLoadGameKeys;
    [HideInInspector]
    public string goldIdfaKey = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("goldContextViewInfoSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { goldIdfaKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(GoldInit), 5f);
    }

    private void GoldInit()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        GoldGameNextInit(data);
    }

    private void GoldGameNextInit(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("goldMiniGameDataSave", string.Empty) != string.Empty)
            {
                FindAnyObjectByType<GoldAdditionalLoadGameHelper>().GoldLoadMiniGame(PlayerPrefs.GetString("goldMiniGameDataSave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in goldLoadGameKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(FindAnyObjectByType< GoldAdditionalLoadGameHelper >(). LaunchGoldGameInitialization(stringtemp, data));
            }
        }
        else
        {
            FindAnyObjectByType<GoldAdditionalLoadGameHelper>().GoldLOadGame();
        }
    }
}
