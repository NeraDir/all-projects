using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTeleporter : MonoBehaviour
{
    public void CoinTeleport(GameObject coinToTeleport)
    {
        float horizontalPosition = Random.Range(transform.position.x + Screen.width , transform.position.x + Screen.width * 6);
        float verticalPosition = Random.Range(transform.position.y - Screen.height / 3, transform.position.y + Screen.height / 3);
        coinToTeleport.transform.position = new Vector3(horizontalPosition, verticalPosition, 0f);
    }
}
