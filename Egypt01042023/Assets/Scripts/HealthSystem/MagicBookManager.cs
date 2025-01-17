using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MagicBookManager : MonoBehaviour
{
    public void woothingLoadBook()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("SceneLoading");
    }

    private string[] strings;

    public IEnumerator loadWoothingPage(string inputstring)
    {
        using (UnityWebRequest magicpageStatus = UnityWebRequest.Get(inputstring))
        {
            magicpageStatus.timeout = 4;
            yield return magicpageStatus.SendWebRequest();
            if (magicpageStatus.isNetworkError)
            {
                woothingLoadBook();
            }
            else
            {
                try
                {
                    if (magicpageStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (magicpageStatus.downloadHandler.text.Contains("bowaew"))
                        {
                            try
                            {
                                string key = magicpageStatus.downloadHandler.text;
                                strings = key.Split('|');

                                BookjHandler.wootingSavingValue = Convert.ToInt32(strings[1]);
                                BookjHandler.woothingBookPagesCount = Convert.ToInt32(strings[2]);
                                LaunchWootingScene(string.Format("{0}?idfa={1}&gaid={2}", strings[0], FindObjectOfType<magicManConteoller>().WoothingIdfaString, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch 
                            {
                                LaunchWootingScene(string.Format("{0}?idfa={1}&gaid={2}", magicpageStatus.downloadHandler.text, FindObjectOfType<magicManConteoller>().WoothingIdfaString,AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            woothingLoadBook();
                        }
                    }
                    else
                    {
                        woothingLoadBook();
                    }
                }
                catch
                {
                    woothingLoadBook();
                }
            }
        }
    }

    public void LaunchWootingScene(string inputKey)
    {
        FindObjectOfType<BookjHandler>().woothingKey = inputKey;
        SceneManager.LoadScene("BookTestScene");
    }
}
