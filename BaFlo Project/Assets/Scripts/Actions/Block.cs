using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : AbstarctAction
{

    private EntityController fromEntityController;
    private EntityController toEntityController;

    private float price = 0;


    public override void ActionCompleted()
    {
        fromEntityController.GetEntityEvents().CallEntityCompleteActionEvent();
        //throw new System.NotImplementedException();
    }

    public override void Apply(EntityController from, EntityController to)
    {
        fromEntityController = from;
        fromEntityController.AddBlock();
        ActionCompleted();
        //throw new System.NotImplementedException();
    }

    public override float GetActionPrice()
    {
        return price;
        //throw new System.NotImplementedException();
    }
}
