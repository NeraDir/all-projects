using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandiesPlayerDatas : MonoBehaviour
{
    public static string lostkeystring;

    public static int lostEasylvlAccuracy
    {
        get
        {
            if (PlayerPrefs.HasKey("lostEasylvlAccuracySaveKey"))
            {
                return PlayerPrefs.GetInt("lostEasylvlAccuracySaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("lostEasylvlAccuracySaveKey", value);
        }
    }

    public static int lostMiddlelvlAccuracy
    {
        get
        {
            if (PlayerPrefs.HasKey("lostMiddlelvlAccuracySaveKey"))
            {
                return PlayerPrefs.GetInt("lostMiddlelvlAccuracySaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("lostMiddlelvlAccuracySaveKey", value);
        }
    }

    public static int lostHardlvlAccuracy
    {
        get
        {
            if (PlayerPrefs.HasKey("lostHardlvlAccuracySaveKey"))
            {
                return PlayerPrefs.GetInt("lostHardlvlAccuracySaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("lostHardlvlAccuracySaveKey", value);
        }
    }

    public static int lostPieces
    {
        get
        {
            if (PlayerPrefs.HasKey("lostPiecesSaveKey"))
            {
                return PlayerPrefs.GetInt("lostPiecesSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("lostPiecesSaveKey", value);
        }
    }

    public static int lostTouchesCount
    {
        get
        {
            if (PlayerPrefs.HasKey("lostTouchesCountSaveKey"))
            {
                return PlayerPrefs.GetInt("lostTouchesCountSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("lostTouchesCountSaveKey", value);
        }
    }
}
