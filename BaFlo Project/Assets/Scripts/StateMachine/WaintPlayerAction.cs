using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaintPlayerAction : GameState
{
    private GameController parent;

    public override void EnterState(GameController gameController)
    {
        parent = gameController;

        UI_GamePlayPage.ActionSelectedEvent += SetActionToPlayer;


        parent.playerController.SetRival(parent.currentEnemyController);
        parent.uI_GamePlayPage.ShowActionButtonsPanel();

        parent.targetLabelIcon.transform.position = parent.playerController.transform.position;
        parent.targetLabelIcon.gameObject.SetActive(true);

        if (parent.playerController.gameObject.TryGetComponent(out EnergyRecovery energyRecovery))
        {
            energyRecovery.canRecover = false;
        }
    }

    public override void ExitState()
    {
        UI_GamePlayPage.ActionSelectedEvent -= SetActionToPlayer;

        if (parent.targetLabelIcon.gameObject != null)
        {
            parent.targetLabelIcon.gameObject.SetActive(false);
        }

        if (parent.playerController != null)
        {
            if (parent.playerController.gameObject.TryGetComponent(out EnergyRecovery energyRecovery))
            {
                energyRecovery.canRecover = true;
            }
        }


    }

    public override void StateAction()
    {
        //throw new System.NotImplementedException();
    }

    public void SetActionToPlayer(ActionButtonTypes actionTypes)
    {
        if (actionTypes == ActionButtonTypes.Attack)
        {
            parent.playerController.action = parent.attack;
        }
        else if (actionTypes == ActionButtonTypes.Block)
        {
            parent.playerController.action = parent.block;
        }
        else if (actionTypes == ActionButtonTypes.FieryRain)
        {
            parent.playerController.action = parent.fieryRainActionPanel;
        }
        else if (actionTypes == ActionButtonTypes.PoisonRain)
        {
            parent.playerController.action = parent.poisonRainActionPanel;
        }

        parent.ChangeState(new PerformingPlayerAction(), 0.1f);
    }


}

