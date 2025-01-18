
using UnityEngine;

public class GleamingContainer : MonoBehaviour
{
    public string gleamingSceneName;

    public static int gleamingCurrentSavingValue
    {
        get
        {
            if (PlayerPrefs.HasKey("gleamingCurrentSavingValue"))
            {
                return PlayerPrefs.GetInt("gleamingCurrentSavingValue");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("gleamingCurrentSavingValue", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
