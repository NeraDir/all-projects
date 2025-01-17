using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEvents : MonoBehaviour
{
    public delegate void EntityDeathDelegate();
    public event EntityDeathDelegate EntityDeadEvent;

    public delegate void EntityAttackDelegate();
    public event EntityAttackDelegate EntityCompleteActionEvent;



    public void CallEntityDeadEvent()
    {
        if (EntityDeadEvent != null)
            EntityDeadEvent();
    }
    public void CallEntityCompleteActionEvent()
    {
        if (EntityCompleteActionEvent != null)
            EntityCompleteActionEvent();
    }

}
