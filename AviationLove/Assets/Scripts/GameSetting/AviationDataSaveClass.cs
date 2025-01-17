using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AviationDataSaveClass : MonoBehaviour
{
    public static float AviationLoveMoneys 
    {
        get
        {
            if (PlayerPrefs.HasKey("JatpackMoneyCountStateSave"))
            {
                return PlayerPrefs.GetFloat("JatpackMoneyCountStateSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetFloat("JatpackMoneyCountStateSave", value);
        }
    }
}
