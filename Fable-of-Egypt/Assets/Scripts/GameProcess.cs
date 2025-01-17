using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess : MonoBehaviour
{
    [SerializeField] private GameCreator gameCreator;
    [SerializeField] private ArrowChecker arrowChecker;
    [SerializeField] private ArrowMovement arrowMovement;
    [SerializeField] private Wallet wallet;
    [SerializeField] private UIState uIState;

    private void Awake()
    {
        arrowChecker.event_IsCheck += gameCreator.OnResutOfShoot;
        gameCreator.event_IsReloaded += arrowMovement.Movement;
        //gameCreator.event_IsEndResult += OnEndResult;
    }

    private void Start()
    {
       // OnClickOpenLevel(1);
    }

    public void OnClickOpenLevel(int level)
    {
        gameCreator.CreateGame(level);
        uIState.CloseAll();
    }

    public void OnClickShot()
    {
        arrowMovement.Movement(false);
        arrowChecker.OnCheck();
    }

    public void OnEndResult(bool isWin)
    {
        uIState.OpenMainMenu();
        Wallet.instance.ReloadWallet();
    }
}
