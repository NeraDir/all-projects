using System.Collections;
using UnityEngine;

public class GoodRinger : MonoBehaviour
{
    private bool isTriggered;

    public void OnTriggeredSizer() 
    {
        PlaneMove.passedParts++;
        StartCoroutine(ChangeSacle());
    }

    private IEnumerator ChangeSacle() 
    {
        while (transform.parent.localScale.x != 0) 
        {
            transform.parent.localScale = Vector3.MoveTowards(transform.parent.localScale, Vector3.zero, 20 * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject.transform.parent.gameObject);
    }
}
