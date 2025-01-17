public abstract class EntityState
{
    public abstract void EnterState(PlayerMnagaer _parrent);
    public abstract void ExitState(EntityStateEnum _nextState);
    public abstract void StateLogic();

    public abstract EntityStateEnum GetState();
}

public enum EntityStateEnum
{
    Idle,
    Run,
    Roll,
    Attack,
    Die
}