using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class AbstarctEntityInformationModifire : MonoBehaviour
{
    [SerializeField]
    private TMP_Text entityHealthText;
    [SerializeField]
    private TMP_Text entityEnergyText;
    [SerializeField]
    private TMP_Text entityDamageValueText;
    [SerializeField]
    private TMP_Text entityLevelNumberText;

    private EntityInformation currentEnityInformation;

    private float entityHealthValueLerp;
    private float entityEnergyValueLerp;


    private void OnEnable()
    {
        entityHealthValueLerp = 0;
        entityEnergyValueLerp = 0;
    }


    public void SetInfo(EntityInformation entityInformation)
    {
        this.currentEnityInformation = entityInformation;

        if (entityDamageValueText != null)
            entityDamageValueText.text = currentEnityInformation.damageValue.ToString();
        if (entityLevelNumberText != null)
            entityLevelNumberText.text = currentEnityInformation.LevelNumber.ToString();
    }



    private void Update()
    {
        if (currentEnityInformation != null)
        {
            if (entityHealthText != null)
            {
                entityHealthValueLerp = Mathf.Lerp(entityHealthValueLerp, currentEnityInformation.HealthValue, 0.3f);
                entityHealthText.text = entityHealthValueLerp.ToString("#") + "/" + currentEnityInformation.maxHealthValue;
            }

            if (entityEnergyText != null)
            {
                entityEnergyValueLerp = Mathf.Lerp(entityEnergyValueLerp, currentEnityInformation.EnergyValue, 0.3f);
                entityHealthText.text = entityEnergyValueLerp.ToString("#") + "/" + currentEnityInformation.maxEnergyValue;
            }
        }
    }
}
