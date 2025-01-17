using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityEvents), typeof(EntityAnimationController), typeof(EntityInformation))]
public class EntityController : MonoBehaviour
{
    private EntityInformation entityInformation;
    private EntityEvents entityEvents;
    private EntityAnimationController entityAnimationController;

    [HideInInspector]
    public AbstarctAction action;


    private EntityController rivalCurrentEntity;

    private bool hasBlock;

    private void OnEnable()
    {
        entityInformation = GetComponent<EntityInformation>();
        entityEvents = GetComponent<EntityEvents>();
        entityAnimationController = GetComponent<EntityAnimationController>();

    }


    public void PerformAnAction()
    {
        action.Apply(this, rivalCurrentEntity);
    }

    public void TakeDamage(float damageValue)
    {
        if (hasBlock)
        {
            hasBlock = false;
            damageValue *= 0.5f;
        }

        if (entityInformation.HealthValue - damageValue > 0)
        {
            entityInformation.HealthValue -= damageValue;
        }
        else
        {
            entityInformation.HealthValue = 0;
            entityAnimationController.PlayDeathAnimation();
        }
    }


    public void AttackAnimationCompleted()
    {
        action.ActionCompleted();
        entityAnimationController.PlayIdleAnimation();

    }
    public void DeathAnimationCompleted()
    {
        entityEvents.CallEntityDeadEvent();
    }

    public void SetRival(EntityController rivalEntity)
    {
        rivalCurrentEntity = rivalEntity;
    }


    public EntityInformation GetEntityInformation()
    {
        return entityInformation;
    }
    public EntityEvents GetEntityEvents()
    {
        return entityEvents;
    }
    public EntityAnimationController GetEntityAnimationController()
    {
        return entityAnimationController;
    }


    public void AddBlock()
    {
        hasBlock = true;
    }
}
