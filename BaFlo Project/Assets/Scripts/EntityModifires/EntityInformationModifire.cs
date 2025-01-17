using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityInformationModifire : MonoBehaviour
{
    [SerializeField]
    private TMP_Text entityNameText;

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
       

        if (currentEnityInformation != null)
        {
            currentEnityInformation.HealthValueChanged -= ShowHealtDicrement;
            currentEnityInformation.EnergyValueChanged -= ShowEnergyDicrement;
        }

        this.currentEnityInformation = entityInformation;

        entityNameText.text = currentEnityInformation.entityNameString;
        entityNameText.color = currentEnityInformation.entityNameStringColor;

        if (entityDamageValueText != null)
            entityDamageValueText.text = currentEnityInformation.damageValue.ToString();
        if (entityLevelNumberText != null)
            entityLevelNumberText.text = "LVL." + currentEnityInformation.LevelNumber.ToString();


        currentEnityInformation.HealthValueChanged += ShowHealtDicrement;
        currentEnityInformation.EnergyValueChanged += ShowEnergyDicrement;

        if (currentEnityInformation.informationPanelPosPointInScreen != null)
        {
            transform.position = new Vector3(transform.position.x, currentEnityInformation.informationPanelPosPointInScreen.position.y, transform.position.z);
        }

        //transform.localPosition = this.currentEnityInformation.informationPanelPosInScreen;
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
                entityEnergyText.text = entityEnergyValueLerp.ToString("#") + "/" + currentEnityInformation.maxEnergyValue;
            }
        }
    }

    public void ShowHealtDicrement(float value)
    {
        //Debug.Log("HealthDicrement: " + value);
    }
    public void ShowEnergyDicrement(float value)
    {
        //Debug.Log("EnergyDicrement: " + value);
    }

}
