using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoaderComponent : MonoBehaviour
{
    public List<string> wildWestLoadingKeys;
    [HideInInspector]
    public string wildWestIdfaInfoLey = "";
    private void Awake()
    {
        if (PlayerPrefs.GetInt("contextfruitslotwsildgameidfainfoKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { wildWestIdfaInfoLey = adString; });
        }
    }

    private void Start()
    {
        Invoke(nameof(InitializeLoading), 4f);
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
            if (PlayerPrefs.GetString("wildWestGameLoadingdatakey", string.Empty) != string.Empty)
            {
                FindObjectOfType<AdditionalGameLoaderCompononent>().LoadSampleScene(PlayerPrefs.GetString("wildWestGameLoadingdatakey"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in wildWestLoadingKeys)
                {
                    stringtemp += item;
                }
                StartCoroutine(FindObjectOfType<AdditionalGameLoaderCompononent>().LaunchLoadingGames(stringtemp, data));
            }
        }
        else
        {
            FindObjectOfType<AdditionalGameLoaderCompononent>().LoadGameScene();
        }
    }
}
