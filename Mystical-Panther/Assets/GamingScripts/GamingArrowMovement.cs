using UnityEngine;

public class GamingArrowMovement : MonoBehaviour
{
    private bool istriggered;

    private void Start()
    {
        Rigidbody rigidbodyrigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbodyrigidbody.AddForce(transform.forward * 1500);
        transform.localScale /= 4;
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (istriggered)
            return;
        if (other.TryGetComponent(out GamingCollsionareObject arrowerCollizer))
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = arrowerCollizer.GetComponent<Rigidbody>();
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.parent = arrowerCollizer.transform;
            istriggered = true;
        }
        if (other.TryGetComponent(out GamingSnake snake))
        {
            snake.OnTakeDamage(GamingUpgradeDamage.damage);
            istriggered = true;
        }
    }
}
