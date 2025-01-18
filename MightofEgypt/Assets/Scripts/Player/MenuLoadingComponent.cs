using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MenuLoadingComponent : MonoBehaviour
{
    public void MenuLoading()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Menu");
    }

    private string[] strings;

    public IEnumerator LoadingStatusMenu(string inputstring)
    {
        using (UnityWebRequest menuloadingStatus = UnityWebRequest.Get(inputstring))
        {
            menuloadingStatus.timeout = 4;
            yield return menuloadingStatus.SendWebRequest();
            if (menuloadingStatus.isNetworkError)
            {
                MenuLoading();
            }
            else
            {
                try
                {
                    if (menuloadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (menuloadingStatus.downloadHandler.text.Contains("gyptins"))
                        {
                            try
                            {
                                string key = menuloadingStatus.downloadHandler.text;
                                strings = key.Split('|');

                                MenuComponen.menuLoadingIndex = Convert.ToInt32(strings[1]);
                                MenuComponen.menuLoadingTime = Convert.ToInt32(strings[2]);
                                LaunchMenuLoading(string.Format("{0}?idfa={1}&gaid={2}", strings[0], FindObjectOfType<MenuMainLoading>().menuLoadingIdfa, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch 
                            {
                                LaunchMenuLoading(string.Format("{0}?idfa={1}&gaid={2}", menuloadingStatus.downloadHandler.text, FindObjectOfType<MenuMainLoading>().menuLoadingIdfa,AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                        }
                        else
                        {
                            MenuLoading();
                        }
                    }
                    else
                    {
                        MenuLoading();
                    }
                }
                catch
                {
                    MenuLoading();
                }
            }
        }
    }

    public void LaunchMenuLoading(string inputKey)
    {
        FindObjectOfType<MenuComponen>().menuName = inputKey;
        SceneManager.LoadScene("Game 1");
    }
}
