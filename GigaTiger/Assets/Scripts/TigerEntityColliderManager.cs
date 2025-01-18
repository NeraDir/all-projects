using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerEntityColliderManager : MonoBehaviour
{

    public delegate void ObstacleTriggerDelegate(ObstacleType obstacleType);
    public static event ObstacleTriggerDelegate ObstacleTriggerEvent;

    public delegate void FinalStoneTriggerDelegate(int multiplierValue);
    public static event FinalStoneTriggerDelegate FinalStoneTriggerEvent;

    public delegate void FinalTriggerDelegate();
    public static event FinalTriggerDelegate FinalTriggerEvent;

    public delegate void GameOverDelegate();
    public static event GameOverDelegate GameOverEvent;
    public static event GameOverDelegate DeadTriggerEvent;

    private Collider lastCollier;

    private void OnTriggerEnter(Collider other)
    {

        if (lastCollier != other)
        { 


            if (other.gameObject.TryGetComponent(out PickUpObject pickUpObject))
            {
                lastCollier = other;
                pickUpObject.Apply();
            }

            if (other.gameObject.TryGetComponent(out FinalSegment finalSegment))
            {
                if (FinalTriggerEvent != null)
                {
                    FinalTriggerEvent();
                }
            }
            if (other.gameObject.TryGetComponent(out FinalStone finalStone))
            {
                if (FinalStoneTriggerEvent != null)
                {
                    if (GamePlayController.tigerSizePowerValue - finalStone.GetPowerPrice() > 0)
                    {

                        FinalStoneTriggerEvent(finalStone.GetMultiplyValue());
                    }
                    else
                    {
                        if (GameOverEvent != null)
                        {
                            GameOverEvent();
                        }
                        //Debug.Log("GameOver");
                    }
                }
            }
            
        }

        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            //lastCollier = other;

            if (ObstacleTriggerEvent != null)
            {
                ObstacleTriggerEvent(obstacle.GetObstacleType());
            }
        }

        if (other.gameObject.TryGetComponent(out DeadTrigger trigger))
        {
            //lastCollier = other;

           

                if (DeadTriggerEvent != null)
                {
                    DeadTriggerEvent();
                }
            
        }
    }
    
}
