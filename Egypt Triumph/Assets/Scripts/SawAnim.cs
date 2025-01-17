using UnityEngine;

public class SawAnim : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 0, 180 * Time.deltaTime);
    }
}
