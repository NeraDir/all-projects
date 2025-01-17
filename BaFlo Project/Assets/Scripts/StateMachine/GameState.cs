using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{

    public abstract void EnterState(GameController gameController);
    public abstract void StateAction();
    public abstract void ExitState();

}
