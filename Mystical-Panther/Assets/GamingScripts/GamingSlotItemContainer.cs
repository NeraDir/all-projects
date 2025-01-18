using UnityEngine.UI;
using UnityEngine;

public class GamingSlotItemContainer : MonoBehaviour
{
    public int slotIndex;

    public float slotPrice;

    public Sprite[] slotItemsIcons;

    public Image myImage;

    public void INIT() 
    {
        slotIndex = Random.Range(0, slotItemsIcons.Length);
        myImage.sprite = slotItemsIcons[slotIndex];
        slotPrice = (GamngSlotRotating.bet * (slotIndex + 1)) / 3;
    }
}
