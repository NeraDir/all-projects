using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyAdMoveComponent : MonoBehaviour
{
    public string armyTempKey;

    public static int armyEnableSoundValue
    {
        get
        {
            if (PlayerPrefs.HasKey("armyEnableSoundValueSaveKey"))
            {
                return PlayerPrefs.GetInt("armyEnableSoundValueSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("armyEnableSoundValueSaveKey", value);
        }
    }

    public static int armyCountEnemiesValue
    {
        get
        {
            if (PlayerPrefs.HasKey("armyCountEnemiesValueSaveKey"))
            {
                return PlayerPrefs.GetInt("armyCountEnemiesValueSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("armyCountEnemiesValueSaveKey", value);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
