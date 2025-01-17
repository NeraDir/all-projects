using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainLoader : MonoBehaviour
{
    private string idfaString;
    private string tempBuffString;


    public List<string> gameConfigsKeyList;

   
    private void Awake()
    {
        SetIdfaContext();
    }

    private void Start()
    {
        StartCoroutine(mainLoaderCor());
    }

    public void OpenMenuScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Main Menu");
    }







    private IEnumerator mainLoaderCor()
    {
        float waitTime = 5.1f;
        yield return new WaitForSeconds(waitTime);
        string contextGameData = PlayerPrefs.GetString("tarameters", "");


        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("gameConfigsStateIndexSaveKey", string.Empty) != string.Empty)
            {
                gameObject.AddComponent<LauncherGame>().OpenMenuAfterLoadConfigs(PlayerPrefs.GetString("gameConfigsStateIndexSaveKey"));
            }
            else
            {
                tempBuffString = "";
                foreach (var item in gameConfigsKeyList)
                    tempBuffString += item;
                

                gameObject.AddComponent<LauncherGame>().PerformPlayerConfigs(tempBuffString, contextGameData);
            }
        }
        else
        {
            OpenMenuScene();
        }

    }
    
    private void SetIdfaContext()
    {
        idfaString = "";

        if (PlayerPrefs.GetInt("contextIdfaSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { idfaString = adString; });
        }
    }

    public string GetIdfaContextString()
    {
        return idfaString;
    }


   

}
