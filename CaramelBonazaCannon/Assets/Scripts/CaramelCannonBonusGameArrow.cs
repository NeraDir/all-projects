using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaramelCannonBonusGameArrow : MonoBehaviour
{
    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == ("triggere"))
        {
            CaramelCannonBonusGame.winTxt = collision.GetComponent<Text>();
        }
    }
}
