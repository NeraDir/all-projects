using UnityEngine;

public class GoalKeeperRotration : MonoBehaviour
{
    [SerializeField] private Transform ball;

    private void Update()
    {
        if (ball.transform.position.z < 50.2f) 
        {
            Vector3 direction = ball.position - transform.position;
            transform.forward = new Vector3(direction.x, 0, direction.z);
        }
    }
}
