using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemieHealth : MonoBehaviour
{
    public TMP_Text showhp;

    private void LateUpdate()
    {
        showhp.text = aviaEnemie.enemieHealth.ToString("0");
    }
}
