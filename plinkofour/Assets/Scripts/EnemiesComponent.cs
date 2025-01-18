using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesComponent : MonoBehaviour
{
    public bool canDoAnyting;

    private bool isTriggered;

    private void LateUpdate()
    {
        if (!canDoAnyting)
            return;
        transform.Rotate(new Vector3(0, 0, 1), 360 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallAdditionalComponent ball1) || other.TryGetComponent(out BallComponent ball))
        {
            if (isTriggered)
                return;
            isTriggered = true;
            BallComponent.removePart?.Invoke();
            Destroy(gameObject);
        }
    }
}
