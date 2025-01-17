using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitComponent : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _moveSpeed;

    private bool _isTriggered;

    public bool CanTrigger;

    public int knifesCount;

    private void LateUpdate()
    {
        transform.RotateAround(_target.position, new Vector3(0,0,1), _moveSpeed * Time.deltaTime);
    }

    public void Use()
    {
        if (!CanTrigger)
            return;
        if (_isTriggered) 
            return;
        _isTriggered = true;
        StartCoroutine(Using());
    }

    private IEnumerator Using()
    {
        while (transform.localScale != Vector3.zero)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 100 * Time.deltaTime);
            yield return null;
        }
        FruitMainGameManager._fruitsComponents.Remove(this);
        Destroy(gameObject);
    }
}
