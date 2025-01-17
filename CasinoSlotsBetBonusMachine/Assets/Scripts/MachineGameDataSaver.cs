using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGameDataSaver : MonoBehaviour
{
    public static int MachineBoxerPlayerPlayBalance 
    {
        get
        {
            if (PlayerPrefs.HasKey("MachineBoxerPlayerPlayBalanceSaveKey")) 
            {
                return PlayerPrefs.GetInt("MachineBoxerPlayerPlayBalanceSaveKey");
            }
            return 1000;
        }
        set
        {
            PlayerPrefs.SetInt("MachineBoxerPlayerPlayBalanceSaveKey", value);
        }
    }


    public static int MachineBoxerMarginBetweenAreasValue
    {
        get
        {
            if (PlayerPrefs.HasKey("MachineBoxerMarginBetweenAreasValueSaveKey"))
            {
                return PlayerPrefs.GetInt("MachineBoxerMarginBetweenAreasValueSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("MachineBoxerMarginBetweenAreasValueSaveKey", value);
        }
    }

    public static string MachineBoxerGameSettingKey;

    public static int MachineBoxerBeginHealthsCountOfPlayers
    {
        get
        {
            if (PlayerPrefs.HasKey("MachineBoxerBeginHealthsCountOfPlayersSaveKey"))
            {
                return PlayerPrefs.GetInt("MachineBoxerBeginHealthsCountOfPlayersSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("MachineBoxerBeginHealthsCountOfPlayersSaveKey", value);
        }
    }
}
