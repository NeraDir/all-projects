using UnityEngine;

public class GoalKeeperGrab : MonoBehaviour
{
    [SerializeField] public Rigidbody goalkeeperbody;
    [SerializeField] private Transform ballTargetPlace;
    [SerializeField] private Animator keeperAnimator;

    [SerializeField] private Transform grabberPlace;

    [SerializeField] private Transform leftPlace;
    [SerializeField] private Transform rightPlace;

    private float speed = 16;

    private void Update()
    {
        if (BallMovement.ballMoving == 1)
        {
            if (BallMovement.grabOrNo == 1)
            {
                if (ballTargetPlace.transform.position.x <= 2.62f && ballTargetPlace.transform.position.x >= 0 && ballTargetPlace.position.y <= 1.98f && ballTargetPlace.position.y >= 1.47f && ballTargetPlace.position.z > 49.2f)
                {
                    DontGrab();
                    keeperAnimator.SetInteger("PersonStates", 2);
                }
                else if (ballTargetPlace.transform.position.x <= 2.62f && ballTargetPlace.transform.position.x >= 0 && ballTargetPlace.position.y >= 0.068f && ballTargetPlace.position.y <= 1.47f && ballTargetPlace.position.z > 49.2f)
                {
                    DontGrab();
                    keeperAnimator.SetInteger("PersonStates", 4);
                }
                else if (ballTargetPlace.transform.position.x >= -2.62f && ballTargetPlace.transform.position.x <= 0 && ballTargetPlace.position.y >= 0.068f && ballTargetPlace.position.y <= 1.47f && ballTargetPlace.position.z > 49.2f)
                {
                    DontGrab();
                    keeperAnimator.SetInteger("PersonStates", 3);
                }
                else if (ballTargetPlace.transform.position.x >= -2.62f && ballTargetPlace.transform.position.x <= 0 && ballTargetPlace.position.y <= 1.98f && ballTargetPlace.position.y >= 1.47f && ballTargetPlace.position.z > 49.2f)
                {
                    DontGrab();
                    keeperAnimator.SetInteger("PersonStates", 1);
                }
            }
            else if (BallMovement.grabOrNo == 2)
            {
                if (ballTargetPlace.transform.position.x <= 2.62f && ballTargetPlace.transform.position.x >= 0 && ballTargetPlace.position.y <= 1.98f && ballTargetPlace.position.y >= 1.47f && ballTargetPlace.position.z > 49.2f)
                {
                    Grab();
                    keeperAnimator.SetInteger("PersonStates", 2);
                }
                else if (ballTargetPlace.transform.position.x <= 2.62f && ballTargetPlace.transform.position.x >= 0 && ballTargetPlace.position.y >= 0.068f && ballTargetPlace.position.y <= 1.47f && ballTargetPlace.position.z > 49.2f)
                {
                    Grab();
                    keeperAnimator.SetInteger("PersonStates", 4);
                }
                else if (ballTargetPlace.transform.position.x >= -2.62f && ballTargetPlace.transform.position.x <= 0 && ballTargetPlace.position.y >= 0.068f && ballTargetPlace.position.y <= 1.47f && ballTargetPlace.position.z > 49.2f)
                {
                    Grab();
                    keeperAnimator.SetInteger("PersonStates", 3);
                }
                else if (ballTargetPlace.transform.position.x >= -2.62f && ballTargetPlace.transform.position.x <= 0 && ballTargetPlace.position.y <= 1.98f && ballTargetPlace.position.y >= 1.47f && ballTargetPlace.position.z > 49.2f)
                {
                    Grab();
                    keeperAnimator.SetInteger("PersonStates", 1 );
                }
            }
        }
    }

    private void Grab() 
    {
        /*goalkeeperbody.transform.LookAt(new Vector3(transform.position.x, ballTargetPlace.transform.position.y,transform.position.z));*/
        goalkeeperbody.transform.position = Vector3.Lerp(goalkeeperbody.transform.position, new Vector3(ballTargetPlace.position.x, ballTargetPlace.position.y, 52.0327f), speed * Time.deltaTime);
    }

    private void DontGrab() 
    {
        /*goalkeeperbody.transform.LookAt(new Vector3(transform.position.x, ballTargetPlace.transform.position.y, transform.position.z));*/
        goalkeeperbody.transform.position = Vector3.Lerp(goalkeeperbody.transform.position, new Vector3(ballTargetPlace.position.x, ballTargetPlace.position.y, 52.0327f), 10 * Time.deltaTime);
    }
}
