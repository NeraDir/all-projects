using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplection : GameState
{
    private GameController parent;

    public override void EnterState(GameController gameController)
    {
        parent = gameController;
        GamePlayConfigs.levelNumber++;
        parent.uI_LevelCompletedPage.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        
    }

    public override void StateAction()
    {
        
    }

}
