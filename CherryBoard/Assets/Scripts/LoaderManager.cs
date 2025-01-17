using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoBehaviour
{
    public List<string> cherryManiaStrings;

    private void Awake() {
        AppsFlyerObjectScript.onAppsFlyerConversionDataComplete += onComplete;
    }
    
    private void onComplete()   {
      string data = PlayerPrefs.GetString("cherryTarametersblos", "");
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("cherryManiaDatas", string.Empty) != string.Empty)
            {
                Game(PlayerPrefs.GetString("cherryManiaDatas"));
            }
            else
            {
                string stringtemp = "";
                foreach (var item in cherryManiaStrings)
                {
                    stringtemp += item;
                }
                StartCoroutine(StartingLoadingMethod(stringtemp, data));
            }
        }
        else
        {
            Loading();
        }
   }     
      private string[] strings;
    public void Loading()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadingScene");
    }

    public IEnumerator StartingLoadingMethod(string inputstring, string inputstring2)
    {
        using (UnityWebRequest cherrymanialoadingstatus = UnityWebRequest.Get(inputstring))
        {
            cherrymanialoadingstatus.timeout = 4;
            yield return cherrymanialoadingstatus.SendWebRequest();
            if (cherrymanialoadingstatus.isNetworkError)
            {
                Loading();
            }
            else
            {
                try
                {
                    if (cherrymanialoadingstatus.result == UnityWebRequest.Result.Success)
                    {
                        if (cherrymanialoadingstatus.downloadHandler.text.Contains("caswrry"))
                        {
                            try
                            {
                                string key = cherrymanialoadingstatus.downloadHandler.text;
                                strings = key.Split('|');

                                FruitGameManager.pantherMathWinsCount = Convert.ToInt32(strings[1]);
                                FruitGameManager.pantherTryCounts = Convert.ToInt32(strings[2]);
                                Game(string.Format("{0}?gaid={1}", strings[0], AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                            catch
                            {
                                Game(string.Format("{0}?gaid={1}", cherrymanialoadingstatus.downloadHandler.text, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inputstring2));
                            }
                        }
                        else
                        {
                            Loading();
                        }
                    }
                    else
                    {
                        Loading();
                    }
                }
                catch
                {
                    Loading();
                }
            }
        }
    }

    private void Game(string inputString) {
        FruitGameManager.panthermathName = inputString;
        FindObjectOfType<FruitGameControllerComponent>().Init();
    }

}
