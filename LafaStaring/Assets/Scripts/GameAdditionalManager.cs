using UnityEngine;

public class GameAdditionalManager : MonoBehaviour
{
    public string starringNameKey;

    public static int starringDataSavingValue
    {
        get
        {
            if (PlayerPrefs.HasKey("starringDataSavingValueKEy"))
            {
                return PlayerPrefs.GetInt("starringDataSavingValueKEy");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("starringDataSavingValueKEy", value);
        }
    }

    public static int starringMonstersSaveCount 
    {
        get
        {
            if (PlayerPrefs.HasKey("starringMonstersSaveCountKey"))
            {
                return PlayerPrefs.GetInt("starringMonstersSaveCountKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("starringMonstersSaveCountKey", value);
        }
    }



    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
