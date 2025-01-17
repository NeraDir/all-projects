using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformingPlayerAction : GameState
{
    private GameController parent;
    private GameObject attackIcon;


    public override void EnterState(GameController gameController)
    {
        parent = gameController;
        parent.playerController.GetEntityEvents().EntityCompleteActionEvent += DetectPlayerActionComplete;
        parent.playerController.PerformAnAction();

        attackIcon = parent.attackIcon;

        attackIcon.transform.GetChild(1).gameObject.SetActive(false);
        attackIcon.SetActive(true);
        attackIcon.GetComponent<Animator>().SetInteger("stateIndex", 1);

    }

    public override void ExitState()
    {
        if (parent.playerController != null)
        {
            parent.playerController.GetEntityEvents().EntityCompleteActionEvent -= DetectPlayerActionComplete;

        }

        if (attackIcon != null)
        {
            attackIcon.SetActive(false);
            attackIcon.transform.GetChild(1).gameObject.SetActive(true);
        }

    }

    public override void StateAction()
    {
        //throw new System.NotImplementedException();
    }

    public void DetectPlayerActionComplete()
    {
        if (!parent.playerController.TryGetComponent(out EnergyRecovery energyRecovery))
        {
            parent.playerController.gameObject.AddComponent<EnergyRecovery>();
        }
        

        if (parent.currentEnemyController.GetEntityInformation().HealthValue < 1)
        {
            parent.entityInfomationDisplayModifire.gameObject.SetActive(false);

            if (parent.enemyIndex == parent.GetAllEnemyPrefabs().Count)
            {
                parent.ChangeState(new LevelComplection(), 1.2f);
            }
            else
            {
                parent.ChangeState(new StartFight(), 2f);
            }


        }
        else
        {
            parent.ChangeState(new WaintEnemyAction(), 0.1f);
        }

    }
}
