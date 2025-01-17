using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordUIController : MonoBehaviour
{
    public TMP_Text BestTimeTXT;

    private void Start()
    {
        BestTimeTXT.text = $"Best Time: \n {GameManager.BestTimeSeconds}";
    }
}
