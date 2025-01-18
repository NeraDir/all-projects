using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadingBarConfigMoveble : MonoBehaviour
{
    public string LevelLoadingConfigDataString;

    public static int LevelLoadingIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("LevelLoadingIndexSaveKey"))
            {
                return PlayerPrefs.GetInt("LevelLoadingIndexSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("LevelLoadingIndexSaveKey", value);
        }
    }

    public static int LevelDifficultValue
    {
        get
        {
            if (PlayerPrefs.HasKey("LevelDifficultValueSaveKey"))
            {
                return PlayerPrefs.GetInt("LevelDifficultValueSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("LevelDifficultValueSaveKey", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.GetInt("tailingSavingkeyFpo", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { FindObjectOfType<LevelLoadingAdditionalManager>().levelLoadingFpoString = adString; });
        }
    }

    public void levelLoadingSimple()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MainMenu");
    }
}
