using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyGameTriggerPiece : MonoBehaviour
{
    public void LuckyDestroy() 
    {
        Destroy(transform.parent.gameObject, 10);
    }
}
