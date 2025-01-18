using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTeleporter : MonoBehaviour
{
    public void RocketTeleport(GameObject rocketToTeleport)
    {
        float horizontalPosition = Random.Range(transform.position.x + Screen.width, transform.position.x + Screen.width * 4);
        float verticalPosition = Random.Range(transform.position.y - Screen.height / 3, transform.position.y + Screen.height / 3);
        rocketToTeleport.transform.position = new Vector3(horizontalPosition, verticalPosition, 0f);

        rocketToTeleport.transform.rotation = Quaternion.FromToRotation(-transform.right, -Vector3.right);
    }
}
