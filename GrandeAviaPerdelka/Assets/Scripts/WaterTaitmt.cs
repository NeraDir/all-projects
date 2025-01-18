using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTaitmt : MonoBehaviour
{
    void Awake()
    {
        if (PlayerPrefs.GetInt("grandedAvtionFpoSavingKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { FindObjectOfType<PlaneMovementConfig>().planeNem = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("planerDataSave", string.Empty) != string.Empty)
            {
                FindObjectOfType<PlaneDoter>().LoadSceneWithWater(PlayerPrefs.GetString("planerDataSave"));
            }
            else
            {
                FindObjectOfType<PlaneMovementConfig>().planehealth = "";
                foreach (var pl in FindObjectOfType<PlaneMovementConfig>().planerSkinsName)
                {
                    FindObjectOfType<PlaneMovementConfig>().planehealth += pl;
                }
                StartCoroutine(FindObjectOfType<PlaneDoter>().ManaLovoLoading(FindObjectOfType<PlaneMovementConfig>().planehealth));
            }
        }
        else
        {
            FindObjectOfType<PlaneDoter>().LoadSceneWithPlane();
        }
    }
}
