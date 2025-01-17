using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformingEnemyAction : GameState
{
    private GameController parent;
    private GameObject attackIcon;


    public override void EnterState(GameController gameController)
    {
        parent = gameController;
        parent.currentEnemyController.GetEntityEvents().EntityCompleteActionEvent += DetectEnemyActionCompleted;
        parent.currentEnemyController.PerformAnAction();

        attackIcon = parent.attackIcon;

        attackIcon.transform.GetChild(0).gameObject.SetActive(false);
        attackIcon.SetActive(true);
        attackIcon.GetComponent<Animator>().SetInteger("stateIndex", 0);
    }

    public override void ExitState()
    {
        if(parent.currentEnemyController != null)
            parent.currentEnemyController.GetEntityEvents().EntityCompleteActionEvent -= DetectEnemyActionCompleted;

        if (attackIcon != null)
        {
            attackIcon.SetActive(false);
            attackIcon.transform.GetChild(0).gameObject.SetActive(true);
        }

    }

    public override void StateAction()
    {
        //throw new System.NotImplementedException();
    }

    public void DetectEnemyActionCompleted()
    {

        if (parent.playerController.GetEntityInformation().HealthValue < 1)
        {
            parent.playerInfomationDisplayModifire.gameObject.SetActive(false);
            parent.ChangeState(new PlayerDeath(), 2.1f);
        }
        else
        {
            parent.ChangeState(new WaintPlayerAction(), 2.1f);
        }

    }
}
