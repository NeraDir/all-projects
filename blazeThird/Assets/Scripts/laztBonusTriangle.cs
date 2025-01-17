using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laztBonusTriangle : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out blaztBonusPlace bonusPlace))
        {
            blaztBonusgame.currentMulti =  bonusPlace.multi;
        }
    }
}
