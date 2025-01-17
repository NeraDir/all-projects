using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaintEnemyAction : GameState
{
    private GameController parent;

    public override void EnterState(GameController gameController)
    {
        parent = gameController;
        SetActionToEnemy();

        parent.targetLabelIcon.transform.position = parent.currentEnemyController.transform.position;
        parent.targetLabelIcon.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        if (parent.targetLabelIcon.gameObject != null)
        {
            parent.targetLabelIcon.gameObject.SetActive(false);
        }
        // throw new System.NotImplementedException();
    }

    public override void StateAction()
    {
        //throw new System.NotImplementedException();
    }

    public void SetActionToEnemy()
    {
        parent.currentEnemyController.action = parent.attack;
        parent.ChangeState(new PerformingEnemyAction(), 2f);
    }

}
