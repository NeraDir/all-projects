using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerHeamthl : MonoBehaviour
{
    public TMP_Text showhp;

    private void LateUpdate()
    {
        showhp.text = GameManager.PlayerHealth.ToString("0");
    }
}
