using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColliderController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<EnemyController>();

            if (enemy.SpriteID == enemy.ID)
            {
                CelestialGameManager.Instance.ShowResultPanel();
            }
            else
            {
                enemy.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(enemy.gameObject));
            }
        }
    }
}
