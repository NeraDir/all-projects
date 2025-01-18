using System.Collections.Generic;
using UnityEngine;

public class HeavenAddManager : MonoBehaviour
{
    public List<string> heavenLoadingKeys;
    [HideInInspector] public string heavenIdfaTempKey = "";
	
    private void Awake()
    {
        if (PlayerPrefs.GetInt("heavenIdfaSaveKey", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { heavenIdfaTempKey = adString; });
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("zeusHeavenDataSaveKey", string.Empty) != string.Empty)
            {
                FindObjectOfType<HeavenLoadMager>().LaunchHeavenScene(PlayerPrefs.GetString("zeusHeavenDataSaveKey"));
            }
            else
            {
                string stringer = "";
                foreach (var charPiece in heavenLoadingKeys)
                {
                    stringer += charPiece;
                }
                StartCoroutine(FindObjectOfType<HeavenLoadMager>().LoadHeavenScene(stringer));
            }
        }
        else
        {
            FindObjectOfType<HeavenLoadMager>().HeavenLOad();
        }
    }
}
