using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RevengePlaneCOntroller : MonoBehaviour
{
    public Joystick planeJoystickControllCOmponent;

    public static float planeFuelRevengeValue;

    public Image planeRevengeFuelDisplayBar;

    public TMP_Text planeRevengeFuelValueDisplay;

    private float planeRevengeMaxFuelValue = 100;

    private float planeRevengeFuelDicrementeValue = 1;

    public static UnityEvent planeRevengeFuelEnoughtEvent = new UnityEvent();

    private float revengeTempTimer = 0;

    private void Start()
    {
        planeRevengeMaxFuelValue = 100;
        planeFuelRevengeValue = planeRevengeMaxFuelValue;
    }

    private void LateUpdate()
    {
        transform.position += transform.forward * 6 * Time.deltaTime;
        transform.rotation = Quaternion.Euler(-planeJoystickControllCOmponent.Vertical * 60, planeJoystickControllCOmponent.Horizontal * 60, 0);

        planeRevengeFuelDisplayBar.fillAmount = Mathf.Lerp(planeRevengeFuelDisplayBar.fillAmount, (planeFuelRevengeValue / planeRevengeMaxFuelValue), 10 * Time.deltaTime);
        planeRevengeFuelValueDisplay.text = $"{planeFuelRevengeValue.ToString("0")}/{planeRevengeMaxFuelValue}";
        if (planeFuelRevengeValue >= planeRevengeMaxFuelValue)
        {
            planeFuelRevengeValue = planeRevengeMaxFuelValue;
        }
        revengeTempTimer += Time.deltaTime;
        if (revengeTempTimer >= 5)
        {
            planeRevengeFuelDicrementeValue += 0.5f;
            revengeTempTimer = 0;
        }
        planeFuelRevengeValue -= planeRevengeFuelDicrementeValue * Time.deltaTime;
        if (planeFuelRevengeValue <= 0)
        {
            planeRevengeFuelEnoughtEvent?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICanTriggeredRevenger triggeredObject))
        {
            triggeredObject.OnTriggerUse();
        }
    }
}
