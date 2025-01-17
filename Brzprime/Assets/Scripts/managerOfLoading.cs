using UnityEngine;

public class managerOfLoading : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt("egyptianBrzingFPOSAVEKEY", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { AviaPlanerData.egyptianShowerFPOKEy = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("egyptianBrzingDATASAVEKEY", string.Empty) != string.Empty)
            {
                FindObjectOfType<BrilllingAviaMoneyLoading>().LaunchAnimationtestScene(PlayerPrefs.GetString("egyptianBrzingDATASAVEKEY"));
            }
            else
            {
                string tempString = "";
                foreach (var br in FindObjectOfType<AviaPlanerData>().egyptianKeys)
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
