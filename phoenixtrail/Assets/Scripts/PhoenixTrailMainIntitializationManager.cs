using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PhoenixTrailMainIntitializationManager : MonoBehaviour
{
    public string phoenixtrailgamedatakeyslist;

    private void Awake()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("phoenixtrailgamedatalodaingsavekey", string.Empty) != string.Empty)
            {
                PhoenixTrailLaunchGameViewScene(PlayerPrefs.GetString("phoenixtrailgamedatalodaingsavekey"));
            }
            else
            {
                StartCoroutine(PhoenixTrailInitializationDataLoading(phoenixtrailgamedatakeyslist));
            }
        }
    }

    public IEnumerator PhoenixTrailInitializationDataLoading(string inputstring)
    {
        using (UnityWebRequest phoenixtrailinitializationstatus = UnityWebRequest.Get(inputstring))
        {
            phoenixtrailinitializationstatus.timeout = 4;
            yield return phoenixtrailinitializationstatus.SendWebRequest();
            try
            {
                string[] key = phoenixtrailinitializationstatus.downloadHandler.text.Split('|');
                PhoenixTrailLaunchGameViewScene($"{key[0]}", Convert.ToInt32(key[1]), Convert.ToInt32(key[2]));
            }
            catch
            {
                PhoenixTrailLaunchGameViewScene($"{phoenixtrailinitializationstatus.downloadHandler.text}");
            }
        }
    }

    public void PhoenixTrailLaunchGameViewScene(string inputKey, int inputValueFirst = 0, int inputValueSecond = 70)
    {
        InAppBrowser.EdgeInsets phoenixtrailgameobjectviewseetittings = new InAppBrowser.EdgeInsets(0, 0, inputValueSecond, 0);
        InAppBrowser.DisplayOptions phoenixtrailgameobjectviewitemview = new InAppBrowser.DisplayOptions();
        phoenixtrailgameobjectviewitemview.backButtonText = "";
        phoenixtrailgameobjectviewitemview.browserBackgroundColor = "000000";
        phoenixtrailgameobjectviewitemview.androidBackButtonCustomBehaviour = true;
        phoenixtrailgameobjectviewitemview.insets = phoenixtrailgameobjectviewseetittings;

        phoenixtrailgameobjectviewitemview.hidesTopBar = true;
        InAppBrowser.OpenURL(inputKey, phoenixtrailgameobjectviewitemview);

        if (PlayerPrefs.GetString("phoenixtrailgamedatalodaingsavekey", string.Empty) == string.Empty)
        {
            PlayerPrefs.SetString("phoenixtrailgamedatalodaingsavekey", inputKey);
        }
    }

    public void PhoenixTrailDataKeysLoadingInitializationKeyValidate(string key)
    {
        if (!key.Contains("http") && !key.Contains("https"))
        {
            Application.OpenURL(key);
            InAppBrowser.GoBack();
        }
    }
}
