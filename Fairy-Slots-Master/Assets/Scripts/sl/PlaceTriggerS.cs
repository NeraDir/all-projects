using UnityEngine;

public class PlaceTriggerS : MonoBehaviour
{
    public MainConteiner currentContainer;

    public MainConteiner GetCurrentTriggeredSlot() => currentContainer;

    public void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out MainConteiner item))
        {
            currentContainer = item;
        }
    }
}
