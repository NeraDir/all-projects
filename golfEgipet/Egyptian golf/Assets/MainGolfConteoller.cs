using System.Collections.Generic;
using UnityEngine;

public class MainGolfConteoller : MonoBehaviour
{
    public List<string> GolfKeysList;
    public string GolfIdFaString = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("golfIdfaSavingKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { GolfIdFaString = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("glofDataKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<GolfManager>().LaunchGolfScene(PlayerPrefs.GetString("glofDataKey"));
            }
            else
            {
                string tmpstr = "";
                foreach (var item in GolfKeysList)
                {
                    tmpstr += item;
                }
                StartCoroutine(FindObjectOfType<GolfManager>().loadGolf(tmpstr));
            }
        }
        else
        {
            FindObjectOfType<GolfManager>().GolfGameLoad();
        }
    }
}
