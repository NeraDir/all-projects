using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRecovery : MonoBehaviour
{
    private float recoverSpeed = 2.0f;
    private EntityInformation entityInformation;

    public bool canRecover;

    private void OnEnable()
    {
        canRecover = true;
        entityInformation = GetComponent<EntityInformation>();
        StartCoroutine(recoverEnergy());
    }

    private IEnumerator recoverEnergy()
    {
        while (entityInformation.EnergyValue < entityInformation.maxEnergyValue)
        {
            if (canRecover)
            {
                yield return new WaitForSeconds(recoverSpeed);
                entityInformation.EnergyValue++;

            }
            yield return null;
        }

        Destroy(this);
    }
}
