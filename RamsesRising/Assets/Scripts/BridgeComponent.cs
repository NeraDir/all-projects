using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BridgeComponent : MonoBehaviour, IPointerClickHandler
{
    private bool isClosed;

    public void OnPointerClick(PointerEventData eventData)
    {
        isClosed = !isClosed;
        if (!isClosed)
            transform.Rotate(new Vector3(0,0,1),90);
        else
            transform.Rotate(new Vector3(0, 0, 1), 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CrystallComponent crystall))
        {
            if (!isClosed)
                return;
            crystall.Destroye();
        }
    }
}
