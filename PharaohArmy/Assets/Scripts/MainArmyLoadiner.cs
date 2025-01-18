using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainArmyLoadiner : MonoBehaviour
{
    public List<string> armyStrings;
    public string armytempIdfaString = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("pharaoharmyingState", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { armytempIdfaString = adString; });
        }

        Invoke(nameof(Init), 4);
    }

    private void Init()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("pharaoharmyingDataGameSaveKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<ArmyAddLoadingComponent>().LaunchLoader(PlayerPrefs.GetString("pharaoharmyingDataGameSaveKey"));
            }
            else
            {
                string tempString = "";
                foreach (var item in armyStrings)
                {
                    tempString += item;
                }
                StartCoroutine(FindObjectOfType<ArmyAddLoadingComponent>().LoadGame(tempString));
            }
        }
        else
        {
            FindObjectOfType<ArmyAddLoadingComponent>().LoadLoading();
        }
    }

}
