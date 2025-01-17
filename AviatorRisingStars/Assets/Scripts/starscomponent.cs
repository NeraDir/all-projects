using DG.Tweening;
using UnityEngine;

public class starscomponent : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }

    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 1, 0), 90 * Time.deltaTime);
    }

    public void Use() 
    {
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { Destroy(gameObject);gamemanager.currentCoins++; });
    }
}
