using UnityEngine;

public class EgyptBufferSCC : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt("egyptBufferSCC", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { EgyptAspaScript.EgyptRelKey = adString; });
        }
    }
}
