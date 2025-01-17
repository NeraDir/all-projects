using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstarctAction : MonoBehaviour
{
    public abstract void Apply(EntityController from, EntityController to);
    public abstract void ActionCompleted();
    public abstract float GetActionPrice();

}