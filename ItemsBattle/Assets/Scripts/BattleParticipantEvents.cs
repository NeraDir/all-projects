using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleParticipantEvents : MonoBehaviour
{
    public delegate void RaedyToAttackDelegate();
    public event RaedyToAttackDelegate RaedyToAttackEvent;

    public delegate void ParticipantDeathDelegate();
    public event ParticipantDeathDelegate ParticipantDeadEvent;


    public void CallReadyToAttackEvent()
    {
        if (RaedyToAttackEvent != null)
        {
            RaedyToAttackEvent();
        }
    }

    public void CallParticipantDeadEvent()
    {
        if(ParticipantDeadEvent != null)
        {
            ParticipantDeadEvent();
        }
    }




}
