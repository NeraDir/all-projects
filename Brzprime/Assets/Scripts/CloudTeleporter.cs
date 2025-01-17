using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudTeleporter : MonoBehaviour
{
    public void CloudTeleport(GameObject cloudToTeleport)
    {
        float horizontalPosition = Random.Range(transform.position.x + Screen.width / 2, transform.position.x + Screen.width);
        float verticalPosition = Random.Range(transform.position.y + Screen.height / 8, transform.position.y + Screen.height / 2);
        cloudToTeleport.transform.position = new Vector3(horizontalPosition, verticalPosition, 0f);
    }
}
