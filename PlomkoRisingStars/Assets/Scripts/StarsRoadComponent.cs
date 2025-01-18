using UnityEngine;

public class StarsRoadComponent : MonoBehaviour,IDestroyeble
{
    public void Use()
    {
        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, 0, -1) * 20 * Time.deltaTime;
    }
}
