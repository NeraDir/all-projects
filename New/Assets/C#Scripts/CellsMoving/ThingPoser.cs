using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingPoser : MonoBehaviour
{
    [SerializeField] private Vector2Int mapPosition = Vector2Int.zero;
    [SerializeField] private bool _isGold = true;
    private void Start()
    {
        WorldClockSteps.State3 += Examination;
    }
    private void OnDestroy()
    {
        WorldClockSteps.State3 -= Examination;
    }
    private void Examination()
    {
        if (CharacterControlling.instance.GetPlayerPositionDisposer().GetMapPosition() == mapPosition)
        {
            if (_isGold)
            {
                MainRoundManager.instance.AddGold(10);
                Destroy(gameObject);
                return;
            }
            if (Player.instance.hp < 6)
            {
                Player.instance.Hp(1);
                Destroy(gameObject);
                return;
            }
        }
    }
}
