using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalMiniGameController : MonoBehaviour
{
    [SerializeField]
    private MiniGameController minGameController;

    public void SetNewValue() 
    {
        minGameController.SetNewValue();
    }
}
