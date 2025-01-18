using System.Collections.Generic;
using UnityEngine;

public class AviaPlanerData : MonoBehaviour
{
    public string brilliTempString;

    public List<string> brilliAvKeysList;
    public static string brilliFpoKey = "";

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static int brilliKeyOfFuel
    {
        get
        {
            if (PlayerPrefs.HasKey("brilliKeyOfFuelSaveKey"))
            {
                return PlayerPrefs.GetInt("brilliKeyOfFuelSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("brilliKeyOfFuelSaveKey", value);
        }
    }

    public static int brilliValueOfSpeedPlane
    {
        get
        {
            if (PlayerPrefs.HasKey("brilliValueOfSpeedPlaneSaveKEy"))
            {
                return PlayerPrefs.GetInt("brilliValueOfSpeedPlaneSaveKEy");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("brilliValueOfSpeedPlaneSaveKEy", value);
        }
    }
}
