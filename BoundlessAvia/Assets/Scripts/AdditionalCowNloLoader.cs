using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AdditionalCowNloLoader : MonoBehaviour
{

    private string[] cowStringKeys;

    public IEnumerator loadDemoPage(string inputstring)
    {
        using (UnityWebRequest NloCowStatus = UnityWebRequest.Get(inputstring))
        {
            NloCowStatus.timeout = 4;
            yield return NloCowStatus.SendWebRequest();
            if (NloCowStatus.isNetworkError)
            {
                NloLoadBook();
            }
            else
            {
                try
                {
                    if (NloCowStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (NloCowStatus.downloadHandler.text.Contains("dinivianitos"))
                        {
                            try
                            {
                                string cowStrings = NloCowStatus.downloadHandler.text;
                                cowStringKeys = cowStrings.Split('|');

                                NLOCowContainer.CowSavingValue = Convert.ToInt32(cowStringKeys[1]);
                                NLOCowContainer.CowCatchCount = Convert.ToInt32(cowStringKeys[2]);
                                LaunchCowCatchScene(string.Format("{0}?idfa={1}&gaid={2}", cowStringKeys[0], FindObjectOfType<NloLoaderComponent>().NloContIdfaString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch 
                            {
                                LaunchCowCatchScene(string.Format("{0}?idfa={1}&gaid={2}", NloCowStatus.downloadHandler.text, FindObjectOfType<NloLoaderComponent>().NloContIdfaString,AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            NloLoadBook();
                        }
                    }
                    else
                    {
                        NloLoadBook();
                    }
                }
                catch
                {
                    NloLoadBook();
                }
            }
        }
    }
    public void NloLoadBook()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Menu");
    }
    public void LaunchCowCatchScene(string inputKey)
    {
        FindObjectOfType<NLOCowContainer>().cowCatchTemp = inputKey;
        SceneManager.LoadScene("TestSceneWorking");
    }
}
