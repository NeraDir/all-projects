using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLanEnemieSoul : MonoBehaviour
{
    public void GoTo(Transform lastPosition) 
    {
        transform.DOMoveY(transform.position.y + 2.5f, 0.25f).OnComplete(() =>
        {
            transform.DOMove(lastPosition.position, 0.25f).OnComplete(() =>
            {
                DragonLanController.DragonLanSoulsCount += 1;
                Destroy(gameObject);
            });
        });
    }
}
