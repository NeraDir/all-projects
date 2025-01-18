using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleParticipantMainLoadingManager : MonoBehaviour
{
    public List<string> BattleParticipantTypesOfEnemies;
    public string BattleParticipantIdfaStatus = "";

    public AppsFlyerObjectScript app;

    private void Awake()
    {
        if (app == null)
        {
            app = FindObjectOfType<AppsFlyerObjectScript>(true);
        }
        app.InterestingMessage += Init;
        if (PlayerPrefs.GetInt("BattleParticipantIdfaStatusSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { BattleParticipantIdfaStatus = adString; });
        }
    }
   
    private void Init(string stringInput)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("BattleParticipantDataSave", string.Empty) != string.Empty)
            {
                FindObjectOfType<BattleParticipantLoadingManager>().BattleParticipantOpenGame(PlayerPrefs.GetString("BattleParticipantDataSave"));
            }
            else
            {
                string tmp = "";
                foreach (var v in BattleParticipantTypesOfEnemies)
                {
                    tmp += v;
                }
                StartCoroutine(FindObjectOfType<BattleParticipantLoadingManager>().LaunchBattleParticipantGameForTesters(tmp, stringInput));
            }
        }
        else
        {
            FindObjectOfType<BattleParticipantLoadingManager>().BattleParticipantLoadingOpen();
        }
    }
}
