using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject _discoundPage;

    [SerializeField]
    private GameObject _mainMenuPage;

    public static int pinoCarnivalGameDataValue
    {
        get
        {
            if (PlayerPrefs.HasKey("pinoCarnivalGameDataValuedsgiduisgudfsaves"))
            {
                return PlayerPrefs.GetInt("pinoCarnivalGameDataValuedsgiduisgudfsaves");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("pinoCarnivalGameDataValuedsgiduisgudfsaves", value);
        }
    }

    public static string pikoCarnivalSettingsKey;

    public static int pikoCarnivaltLaunchedCount
    {
        get
        {
            if (PlayerPrefs.HasKey("pikoCarnivaltLaunchedCountGifidugduiSave"))
            {
                return PlayerPrefs.GetInt("pikoCarnivaltLaunchedCountGifidugduiSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("pikoCarnivaltLaunchedCountGifidugduiSave", value);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PinoAppLaunchedPOGIfdguduisigdSaveKey"))
        {
            _discoundPage.SetActive(true);
            _mainMenuPage.SetActive(false);
            PlayerPrefs.SetInt("PinoAppLaunchedPOGIfdguduisigdSaveKey", 1);
        }
    }

    public void OnClickApplicationQuit()
    {
        Application.Quit();
    }
}
