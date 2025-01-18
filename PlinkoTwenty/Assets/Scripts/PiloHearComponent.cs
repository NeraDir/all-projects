using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiloHearComponent : MonoBehaviour, ITakebleComponent
{
    [SerializeField]
    private GameObject _heartsEfffect;

    public void OnTake()
    {
        transform.DOScale(transform.localScale * 1.5f, 0.25f).OnComplete(() => { Destroy(gameObject); Instantiate(_heartsEfffect, transform.position, Quaternion.identity); PiloGameManager.addHeart?.Invoke(); });
    }
}
