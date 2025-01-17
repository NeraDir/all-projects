using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ActionButtonsManager : MonoBehaviour
{

    private Animator myAnimator;

    public delegate void TapActionButtonsDelegate(ActionButtonTypes types);
    public static event TapActionButtonsDelegate TapActionButtonEvent;

    private ActionButtonTypes resultAction;

    private GameController gameController;

    private void Awake()
    {
       // DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnEnable()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void TapAttackActionButton()
    {
        resultAction = ActionButtonTypes.Attack;

        if (CanUseThisAction())
            myAnimator.SetInteger("stateIndex", 1);

    }
    public void TapBlockActionButton()
    {
        resultAction = ActionButtonTypes.Block;

        if (CanUseThisAction())
            myAnimator.SetInteger("stateIndex", 1);
    }
    public void TapFieryRainActionButton()
    {
        resultAction = ActionButtonTypes.FieryRain;

        if (CanUseThisAction())
            myAnimator.SetInteger("stateIndex", 1);
    }
    public void TapPoisonRainActionButton()
    {
        resultAction = ActionButtonTypes.PoisonRain;

        if (CanUseThisAction())
            myAnimator.SetInteger("stateIndex", 1);
    }


    private bool CanUseThisAction()
    {
        float actionPrice = gameController.GetPriceForAction(resultAction);
        float playerEnergyValue = gameController.playerController.GetEntityInformation().EnergyValue;

        if (playerEnergyValue - actionPrice >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    } 


    public void CloseActionButtons()
    {
        if (TapActionButtonEvent != null)
            TapActionButtonEvent(resultAction);
    }

}

public enum ActionButtonTypes
{
    Attack,
    Block,
    FieryRain,
    PoisonRain
}