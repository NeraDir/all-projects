using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : AbstarctAction
{

    private EntityController fromEntityController;
    private EntityController toEntityController;

    private float price = 2;

    public override void Apply(EntityController from, EntityController to)
    {
        fromEntityController = from;
        toEntityController = to;

        fromEntityController.GetEntityAnimationController().PlayAttackAnimation();
    }

    public override void ActionCompleted()
    {
        toEntityController.TakeDamage(fromEntityController.GetEntityInformation().damageValue);
        fromEntityController.GetEntityEvents().CallEntityCompleteActionEvent();
    }

    public override float GetActionPrice()
    {
        return price;
    }
}
