using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWhatIsPacks : MonoBehaviour
{

    public static PackComponent fruitType;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PackComponent pack))
        {
            GameController.CurrentPack = pack;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameController.CurrentPack = null;
    }
}
