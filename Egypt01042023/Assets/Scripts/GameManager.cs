using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text LevelText;
    public TMP_Text ExpirianceText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LevelText.text = "LEVEL: " + CurrentPlayerLevel.ToString();
        ExpirianceText.text = "Exp count: " + ExpirianceSave.ToString();
    }

    public Joystick MovementJoystick;
    public Joystick CameraJoystick;

    public int CurrentPlayerLevel
    {
        get
        {
            if (!PlayerPrefs.HasKey("LovelPlayerKey"))
                return 1;
            else
                return PlayerPrefs.GetInt("LovelPlayerKey");
        }
        set
        {
            PlayerPrefs.SetInt("LovelPlayerKey", value);
            LevelText.text = "LEVEL: " + value.ToString();
        }
    }

    public static float DamageMultiplier
    {
        get
        {
            if (!PlayerPrefs.HasKey("DamageMultiplier"))
                return 0;
            else
                return PlayerPrefs.GetFloat("DamageMultiplier");
        }
        set
        {
            PlayerPrefs.SetFloat("DamageMultiplier", value);
        }
    }

    public static float SpeedMultiplier
    {
        get
        {
            if (!PlayerPrefs.HasKey("SpeedMultiplier"))
                return 0;
            else
                return PlayerPrefs.GetFloat("SpeedMultiplier");
        }
        set
        {
            PlayerPrefs.SetFloat("SpeedMultiplier", value);
        }
    }

    public static float HealthMultiplier
    {
        get
        {
            if (!PlayerPrefs.HasKey("HealthMultiplier"))
                return 0;
            else
                return PlayerPrefs.GetFloat("HealthMultiplier");
        }
        set
        {
            PlayerPrefs.SetFloat("HealthMultiplier", value);
        }
    }

    public int ExpirianceSave
    {
        get
        {
            if (!PlayerPrefs.HasKey("ExpirianceSave"))
                return 0;
            else
                return PlayerPrefs.GetInt("ExpirianceSave");
        }
        set
        {
            if (PlayerPrefs.GetInt("ExpirianceSave") >= CurrentPlayerLevel * 15)
            {
                CurrentPlayerLevel++;
                PlayerPrefs.SetInt("ExpirianceSave", value);
            }
            else
            {
                PlayerPrefs.SetInt("ExpirianceSave", value);
            }

            ExpirianceText.text = "Exp count: " + value.ToString();
        }
    }
}
