using UnityEngine;

public class BrazingTemping : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt("brazinoOk", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { BrzingMovementer.brzingIdfaSaiKey = adString; });
        }
    }
}
