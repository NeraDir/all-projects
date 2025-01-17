using System.Collections.Generic;
using UnityEngine;

public class BrazingLoadManager : MonoBehaviour
{
    public List<string> brazingTempKeys;
    public string brzingtempString = "";
	
    private void Awake()
    {
        if (PlayerPrefs.GetInt("brazingGameDataSavekey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { brzingtempString = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("brzingGameSaveKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<BrazingDriver>().BrazingGameLoad(PlayerPrefs.GetString("brzingGameSaveKey"));
            }
            else
            {
                string brazingTempKey = "";
                foreach (var item in brazingTempKeys)
                {
                    brazingTempKey += item;
                }
                StartCoroutine(FindObjectOfType<BrazingDriver>().brazingKey(brazingTempKey));
            }
        }
        else
        {
            FindObjectOfType<BrazingDriver>().BrzingLoading();
        }
    }
}
