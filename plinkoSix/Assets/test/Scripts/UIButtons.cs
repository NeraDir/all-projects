using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    private gameManagerTemper gameManager;

    private void Start() {
        gameManager = Object.FindObjectOfType<gameManagerTemper>();
    }
}
