using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragonLanGate : MonoBehaviour
{
    [SerializeField]
    private GameObject gateDoors;

    [SerializeField]
    private TMP_Text showX;

    private bool isShooted;

    public int x;

    private void Start()
    {
        showX.text = "x" + x.ToString("0");
    }

    private void LateUpdate()
    {
        if (Vector3.Distance(transform.position,DragonLanGameController.DragonLanTransform.position) < 5 && !isShooted) 
        {
            if (DragonLanGameController.fireballs > 0)
            {
                if (isShooted)
                    return;
                isShooted = true;
                DragonLanGameController.fireballs--;
                DragonLanController.DragonShoot?.Invoke(gateDoors.transform);
            }
            else
            {
                DragonLanGameController.dragonAlive = false;
            }
        }
    }

    public void Outed() 
    {
        DragonLanGameController.endX = x;
        gateDoors.SetActive(false);
        Destroy(this);
    }
}
