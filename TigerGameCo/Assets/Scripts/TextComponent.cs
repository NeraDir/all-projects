using DG.Tweening;
using UnityEngine;

public class TextComponent : MonoBehaviour
{
    private void Start()
    {
        transform.DOScale(Vector3.zero, 2f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
