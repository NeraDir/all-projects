using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startingComponent : MonoBehaviour
{
    public void OnSetStarting() 
    {
        GameManager.isGameStarted = true;
    }
}
