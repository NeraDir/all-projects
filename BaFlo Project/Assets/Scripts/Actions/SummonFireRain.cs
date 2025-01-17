using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonFireRain : AbstarctAction
{
    [SerializeField]
    private float fieryRainDamageValue;

    private EntityController fromEntityController;
    private EntityController toEntityController;

    private float price = 10;

    public override void Apply(EntityController from, EntityController to)
    {
        gameObject.SetActive(true);

        fromEntityController = from;
        toEntityController = to;

        fromEntityController.GetEntityInformation().EnergyValue -= price;
    }


    public override void ActionCompleted()
    {
        toEntityController.TakeDamage(fieryRainDamageValue);
        fromEntityController.GetEntityEvents().CallEntityCompleteActionEvent();
    
        gameObject.SetActive(false);
    }

    public override float GetActionPrice()
    {
        return price;
    }
}
