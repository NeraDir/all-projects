using DG.Tweening;
using UnityEngine;

public class StarsBallComponente : MonoBehaviour,IDestroyeble
{
    public Transform moveTarget;

    public void AddingBall() 
    {
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>{ StarsGameControllerComponent.ballsCount++; Destroy(gameObject); });
        
    }

    public void Use()
    {
        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, 0, -1) * StarsGameControllerComponent.moveObjectsSpeed * Time.deltaTime;
    }
}
