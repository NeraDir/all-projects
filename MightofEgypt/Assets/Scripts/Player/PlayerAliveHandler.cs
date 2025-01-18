using UnityEngine;

public class PlayerAliveHandler : MonoBehaviour
{
    private bool _isPlayerAlive = true;

    public bool IsPlayerAlive{ get => _isPlayerAlive; private set => _isPlayerAlive = value; }

    public void DieOfPlayer() => IsPlayerAlive = false;
}
