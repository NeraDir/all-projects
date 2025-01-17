using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalSave : MonoBehaviour
{
    [SerializeField] private TMP_Text StarsAmount;

    public delegate void UpdateStars();
    public static event UpdateStars UPDStars;

    private void Awake()
    {
        StarsAmount.text = $"x{StarAmount}";
        UPDStars += UPD;
    }

    public void UPD()
    {
        StarsAmount.text = $"x{StarAmount}";
    }

    public static int ChoosenRocket
    {
        get
        {
            if (!PlayerPrefs.HasKey("ChoosenRocket"))
                return 0;
            else
                return PlayerPrefs.GetInt("ChoosenRocket");
        }
        set
        {
            PlayerPrefs.SetInt("ChoosenRocket", value);
        }
    }

    public static int RecordMeteres
    {
        get
        {
            if (!PlayerPrefs.HasKey("RecordMeteresSAve"))
                return 0;
            else
                return PlayerPrefs.GetInt("RecordMeteresSAve");
        }
        set
        {
            if (value > RecordMeteres)
            {
                PlayerPrefs.SetInt("RecordMeteresSAve", value);
            }
        }
    }

    public static int StarAmount
    {
        get
        {
            if (!PlayerPrefs.HasKey("StarAmountSAve"))
                return 0;
            else
                return PlayerPrefs.GetInt("StarAmountSAve");
        }
        set
        {
            PlayerPrefs.SetInt("StarAmountSAve", value);

            if (UPDStars != null)
                UPDStars();
        }
    }


}
