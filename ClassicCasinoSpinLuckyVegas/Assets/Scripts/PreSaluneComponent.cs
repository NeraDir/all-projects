using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreSaluneComponent : MonoBehaviour
{
    public void OnEnd() 
    {

        GameController.playerReachedFinish?.Invoke();
        gameObject.SetActive(false);
    }
}
