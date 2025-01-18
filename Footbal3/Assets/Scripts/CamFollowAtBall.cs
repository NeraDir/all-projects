using UnityEngine;

public class CamFollowAtBall : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private float Smooth;
    [SerializeField] private Vector3 offset1;
    [SerializeField] private Vector3 offset2;
    [SerializeField] private Vector3 offset3;

    private void Update()
    {
        transform.LookAt(ball.position);
        if (BallSpawn._indexOfPositions == 0)
        {
            transform.position = Vector3.Lerp(transform.position, offset1 + ball.transform.position, Smooth * Time.deltaTime);
        }
        else if (BallSpawn._indexOfPositions == 1)
        {
            transform.position = Vector3.Lerp(transform.position, offset2 + ball.transform.position, Smooth * Time.deltaTime);
        }
        else if (BallSpawn._indexOfPositions == 2)
        {
            transform.position = Vector3.Lerp(transform.position, offset3 + ball.transform.position, Smooth * Time.deltaTime);
        }
    }
}
