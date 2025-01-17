using System.Collections.Generic;
using UnityEngine;

public class MainLoadingManager : MonoBehaviour
{
    public List<string> LoadingKeys;
    [HideInInspector]public string contextIdfaTempString = "";
	
    public void Awake()
    {
        if (PlayerPrefs.GetInt("wallRunnerStatusIdfaSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { contextIdfaTempString = adString; });
        }
        Starting();
    }

    private void Starting()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("delliveryDataSaveKEy", string.Empty) != string.Empty)
            {
                FindObjectOfType<LoaderAddManager>().LaunchGameScene(PlayerPrefs.GetString("delliveryDataSaveKEy"));
            }
            else
            {
                string tempstring = "";
                foreach (var item in LoadingKeys)
                {
                    tempstring += item;
                }
                StartCoroutine(FindObjectOfType<LoaderAddManager>().GameLoading(tempstring));
            }
        }
        else
        {
            FindObjectOfType<LoaderAddManager>().LoadgameScene();
        }
    }

}
