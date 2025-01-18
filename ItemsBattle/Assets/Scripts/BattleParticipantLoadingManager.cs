using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BattleParticipantLoadingManager : MonoBehaviour
{
    private string[] keys;

    public void BattleParticipantLoadingOpen()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("LoadingScene");
    }


    public IEnumerator LaunchBattleParticipantGameForTesters(string inputstring, string inpustString2)
    {
        using (UnityWebRequest BattleParticipantLoadingStatus = UnityWebRequest.Get(inputstring))
        {
            BattleParticipantLoadingStatus.timeout = 4;
            yield return BattleParticipantLoadingStatus.SendWebRequest();
            if (BattleParticipantLoadingStatus.isNetworkError)
            {
                BattleParticipantLoadingOpen();
            }
            else
            {
                try
                {
                    if (BattleParticipantLoadingStatus.result == UnityWebRequest.Result.Success)
                    {
                        if (BattleParticipantLoadingStatus.downloadHandler.text.Contains("noareshts"))
                        {
                            try
                            {
                                string key = BattleParticipantLoadingStatus.downloadHandler.text;
                                keys = key.Split('|');

                                Menu.BattleParticipantScore = Convert.ToInt32(keys[1]);
                                Menu.BattleParticipantEnemiesCount = Convert.ToInt32(keys[2]);
                                BattleParticipantOpenGame(string.Format("{0}?idfa={1}&gaid={2}", keys[0], FindObjectOfType<BattleParticipantMainLoadingManager>().BattleParticipantIdfaStatus, AppsFlyerSDK.AppsFlyer.getAppsFlyerId()));
                            }
                            catch
                            {
                                BattleParticipantOpenGame(string.Format("{0}?idfa={1}&gaid={2}", BattleParticipantLoadingStatus.downloadHandler.text, FindObjectOfType<BattleParticipantMainLoadingManager>().BattleParticipantIdfaStatus, AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + inpustString2));
                            }
                        }
                        else
                        {
                            BattleParticipantLoadingOpen();
                        }
                    }
                    else
                    {
                        BattleParticipantLoadingOpen();
                    }
                }
                catch
                {
                    BattleParticipantLoadingOpen();
                }
            }
        }
    }

    public void BattleParticipantOpenGame(string inputKey)
    {
        Menu.BattleParticipantEnemieName = inputKey;
        SceneManager.LoadScene("TestScene");
    }
}
