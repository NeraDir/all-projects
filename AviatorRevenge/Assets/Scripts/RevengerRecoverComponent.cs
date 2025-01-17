using System.Collections;
using UnityEngine;

public class RevengerRecoverComponent : MonoBehaviour, ICanTriggeredRevenger
{
    private bool isDestroying;
    private float startLocalScaleValue;
    public void OnTriggerUse()
    {
        if (isDestroying)
            return;
        isDestroying = true;
        startLocalScaleValue = transform.localScale.x;
        StartCoroutine(Destroying());
    }

    private IEnumerator Destroying()
    {
        RevengerGameManager.revengeDetailsCount += Random.Range(10, 25);
        while (transform.localScale != Vector3.zero)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, startLocalScaleValue * 10 * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
