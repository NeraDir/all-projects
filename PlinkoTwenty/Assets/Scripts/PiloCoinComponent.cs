using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiloCoinComponent : MonoBehaviour,ITakebleComponent
{
    [SerializeField]
    private GameObject _effect;

    public void OnTake()
    {
        transform.DOScale(transform.localScale * 1.5f, 0.25f).OnComplete(() => { Destroy(gameObject); Instantiate(_effect, transform.position, Quaternion.identity);PiloGameManager.addScore?.Invoke(); });
    }

    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1), 90 * Time.deltaTime);
    }
}
