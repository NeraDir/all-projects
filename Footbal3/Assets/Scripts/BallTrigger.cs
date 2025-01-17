using UnityEngine;
using UnityEngine.SceneManagement;

public class BallTrigger : MonoBehaviour
{
    [SerializeField] private Rigidbody ball;
    [SerializeField] private Transform grabPalce;
    [SerializeField] private GoalKeeperRotration gkrotation;
    [SerializeField] private GoalKeeperGrab grab;
    [SerializeField] private PhysicMaterial physics;

    [SerializeField] private GameObject wonLabel;
    [SerializeField] private GameObject looseLabel;

    private bool triggered;
    private void Update() 
    {
        if (triggered)
        {
            physics.bounciness = 0.05f;
        }
        else
        {
            physics.bounciness = 0.3f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GoalPlace>())
        {
            triggered = true;
            grab.enabled = false;
            Invoke(nameof(RestartGame), 3);
            wonLabel.SetActive(true);
            looseLabel.SetActive(false);
            BallMovement.ballMoving = 0;
            BallMovement.rotation = false;
        }
        else if (other.GetComponent<GoalKeeperGrab>() && grab.enabled == true) 
        {
            transform.parent = grabPalce;
            Destroy(this.GetComponent<Rigidbody>());
            Destroy(this.GetComponent<BallMovement>());
            gkrotation.enabled = false;
            BallMovement.ballMoving = 0;
            transform.position = grabPalce.position;
            Invoke(nameof(RestartGame), 3);
            wonLabel.SetActive(false);
            looseLabel.SetActive(true);
        }
        else if (other.GetComponent<Ground>() && BallMovement.rotation && BallMovement.ballMoving == 1 && BallMovement.started)
        {
            BallMovement.ballMoving = 0;
            BallMovement.rotation = false;
            wonLabel.SetActive(false);
            looseLabel.SetActive(true);
            Invoke(nameof(RestartGame), 3);

        }
    }


    private void OnTriggerExit(Collider other) 
    {
        triggered = false;
    }

    private void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
