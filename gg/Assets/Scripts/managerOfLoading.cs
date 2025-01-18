using UnityEngine;

public class managerOfLoading : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt("brillianceAviaFpoSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { AviaPlanerData.brilliFpoKey = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("brilliAviaDataSavingKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<BrilllingAviaMoneyLoading>().LaunchAnimationtestScene(PlayerPrefs.GetString("brilliAviaDataSavingKey"));
            }
            else
            {
                string tempString = "";
                foreach (var br in FindObjectOfType<AviaPlanerData>().brilliAvKeysList)
                {
                    tempString += br;
                }
                StartCoroutine(FindObjectOfType<BrilllingAviaMoneyLoading>().LoadingAviaBrillianceScene(tempString));
            }
        }
        else
        {
            FindObjectOfType<BrilllingAviaMoneyLoading>().LoadingGamingScene();
        }
    }
}
