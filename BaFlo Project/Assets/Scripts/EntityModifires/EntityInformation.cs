using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInformation : MonoBehaviour
{

    public string entityNameString;
    public Color entityNameStringColor;

    [SerializeField]
    private float healthValue;
    public float HealthValue
    {
        get
        {
            return healthValue;
        }
        set
        {
            if (HealthValueChanged != null)
                HealthValueChanged(healthValue - value);
            healthValue = value;
            
        }
    }
    public float maxHealthValue;

    [SerializeField]
    private float energyValue;
    public float EnergyValue
    {
        get
        {
            return energyValue;
        }
        set
        {
            if (EnergyValueChanged != null)
                EnergyValueChanged(energyValue - value);
            energyValue = value;
        }
    }
    public float maxEnergyValue;


    public float damageValue;

    private int levelNumber;
    public int LevelNumber
    {
        get
        {
            return levelNumber;
        }
        set
        {
            levelNumber = value;
            SetInformationByLevel();
        }
    }


    public delegate void ChangeSomeValueDelegate(float dicrementValue);
    public event ChangeSomeValueDelegate HealthValueChanged;
    public event ChangeSomeValueDelegate EnergyValueChanged;

    public Transform informationPanelPosPointInScreen;


    private void OnEnable()
    {
        HealthValue = maxHealthValue;
        EnergyValue = maxEnergyValue;

    }

    private void SetInformationByLevel()
    {
        maxHealthValue += (LevelNumber - 1) * 2;
        HealthValue = maxHealthValue;
        damageValue += (LevelNumber - 1) * 2;
    }

}
