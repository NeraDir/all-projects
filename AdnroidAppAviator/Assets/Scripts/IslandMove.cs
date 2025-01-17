using UnityEngine;

public class IslandMove : MonoBehaviour
{
    [SerializeField]
    private float destroyPos;

    private void Start()
    {
        transform.position = new Vector3(Random.Range(-45f, 45f), -15, 500);
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (1f / 5f * PlayerPrefs.GetInt("speed")));
        if (transform.position.z < destroyPos) Destroy(this.gameObject);
    }

}
