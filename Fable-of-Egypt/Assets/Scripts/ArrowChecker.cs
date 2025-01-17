using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class ArrowChecker : MonoBehaviour
{
    public delegate void Delegate(string id);
    public event Delegate event_IsCheck;

    [SerializeField] private EdgeCollider2D collider;
    [SerializeField] private ContactFilter2D contactFilter;

    [SerializeField] private List<Collider2D> result = new List<Collider2D>();

    public void OnCheck()
    {
        print(collider.OverlapCollider(contactFilter, result));

        if (result.Count > 0)
        {
            Collider2D collision = result[^1];

            if (collision.TryGetComponent(out RawImage image))
            {
                event_IsCheck?.Invoke(image.gameObject.name);
            }
        }
    }


}
