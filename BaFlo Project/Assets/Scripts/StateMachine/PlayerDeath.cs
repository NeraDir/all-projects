using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : GameState
{
    private GameController parent;

    public override void EnterState(GameController gameController)
    {
        parent = gameController;
        parent.uI_GameOverPage.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        //throw new System.NotImplementedException();
    }

    public override void StateAction()
    {
        //throw new System.NotImplementedException();
    }
}
