using DG.Tweening;
using UnityEngine;

public class StarsTurnComponent : MonoBehaviour, IDestroyeble
{
    public StarsBallComponente ball;

    public bool canClick;

    public int turnIndex;

    private void Awake()
    {
        StarsGameControllerComponent.addBall.AddListener(AddBall);
    }

    private void AddBall(int index) 
    {
        if (canClick)
        {
            if (turnIndex == index)
            {
                ball.AddingBall();
                StarsGameControllerComponent.record += Random.Range(5, 10);
                transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
            }
        }
    }

    public void Use()
    {
        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, 0, -1) * StarsGameControllerComponent.moveObjectsSpeed * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out StarsLineDetect line))
        {
            canClick = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canClick = false;
    }
}
