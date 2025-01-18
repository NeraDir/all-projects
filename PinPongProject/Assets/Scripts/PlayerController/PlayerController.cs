using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    RectTransform playerRect;

    private float posX;
    private float posY;

    private void Start()
    {
        playerRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            posX = Input.mousePosition.x; //x
            posY = Input.mousePosition.y; //y

            playerRect.position = new Vector3(posX, playerRect.position.y, playerRect.position.z);
        }
    }
}
