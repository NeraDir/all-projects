using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float destroyPos;

    private void Start()
    {
        transform.position = new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-3.5f, -6f), 20);
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (0.2f / 5f * PlayerPrefs.GetInt("speed")));
        if (transform.position.z < destroyPos) Destroy(this.gameObject);
    }

}
