using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLoaderController : MonoBehaviour
{
    public List<string> magicKeysPanthList;
    [HideInInspector]
    public string MagicIdfaTempKey = "";
   
    private void Awake()
    {
        if (PlayerPrefs.GetInt("magicContextScreenViewStatusInfoSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { MagicIdfaTempKey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(InitializeLoading), 5f);
    }

    private void InitializeLoading()
    {
        string data = PlayerPrefs.GetString("tarameters", "");
        SecondInit(data);
    }

    private void SecondInit(string data)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("magicgamedataSave", string.Empty) != string.Empty)
            {
                FindObjectOfType<MagicAddittionalManager>().MagicLoadMenu(PlayerPrefs.GetString("magicgamedataSave"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in magicKeysPanthList)
                {
                    stringtemp += item;
                }
                StartCoroutine(FindObjectOfType<MagicAddittionalManager>().StartingInitializingGameDatas(stringtemp, data));
            }
        }
        else
        {
            FindObjectOfType<MagicAddittionalManager>().MagicLoadGame();
        }
    }

}
