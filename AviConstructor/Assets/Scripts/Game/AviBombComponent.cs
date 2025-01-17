using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AviBombComponent : MonoBehaviour, IAviTriggerComponent
{
    public int bombHealth = 0;

    private int _bombAviHealth = 90;

    private void Start()
    {
        bombHealth = _bombAviHealth;
    }

    public void Use()
    {
        transform.DOScale(Vector2.zero, 0.25f).OnComplete(() => { AviGameComponent.aviPlanePlayerCurrentHealth -= 10; Destroy(gameObject); });
    }

    private void LateUpdate()
    {
        if (!AviGameComponent.AviGameIsPlay)
            return;
        transform.position += new Vector3(0, -1, 0) * 300 * Time.deltaTime;
    }
}
