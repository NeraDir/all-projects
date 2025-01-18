using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MummyAttackHandler : MonoBehaviour
{
    private const string _playerTag = "Player";

    public UnityEvent OnPlayerCatched;

    [SerializeField] private PlayerAliveHandler _playerAliveHandler;

    private void OnTriggerEnter(Collider other) {
        if (_playerAliveHandler.IsPlayerAlive) {
            switch (other.tag) {
                case _playerTag:
                    OnPlayerCatched.Invoke();
                    break;
            }
        }
    }
}
