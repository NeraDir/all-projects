using UnityEngine;

public class GamingSlotPlaceTrigger : MonoBehaviour
{
    public GamingSlotItemContainer currentContainer;

    public GamingSlotItemContainer GetCurrentTriggeredSlot() => currentContainer;

    public void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out GamingSlotItemContainer item))
        {
            currentContainer = item;
        }
    }
}
