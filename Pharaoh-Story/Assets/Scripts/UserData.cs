using UnityEngine;
using TMPro;

public class UserData : MonoBehaviour
{
    public static int userMoney 
    {
        get 
        {
            if (PlayerPrefs.HasKey("UserMoneyKeySave"))
            {
                return PlayerPrefs.GetInt("UserMoneyKeySave");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("UserMoneyKeySave",value);
        }
    }

    public static int userBestRecord 
    {
        get
        {
            if (PlayerPrefs.HasKey("UserBestRecordKeySave"))
            {
                return PlayerPrefs.GetInt("UserBestRecordKeySave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("UserBestRecordKeySave", value);
        }
    }

    [SerializeField]
    private TMP_Text[] showCurrentScore;

    [SerializeField]
    private TMP_Text showBestRecord;

    private void LateUpdate()
    {
        foreach (var score in showCurrentScore)
            score.text = userMoney.ToString();

        showBestRecord.text = userBestRecord.ToString("0");
    }
}
