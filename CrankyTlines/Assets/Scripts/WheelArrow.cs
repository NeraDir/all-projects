using UnityEngine;

public class WheelArrow : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WheelContainer container))
        {
            DailySpinnerComponent.dailyBonusValue = container.GetValue();
        }
    }
}
