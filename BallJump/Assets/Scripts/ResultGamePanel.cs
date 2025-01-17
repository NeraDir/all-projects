using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultGamePanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text recordMettersText;
    [SerializeField]
    private TMP_Text currentMettersText;
    private float currentMettersValueLerp;

    private void OnEnable()
    {
        currentMettersValueLerp = 0;


        if (BallConfigsController.currentMaxMetters > BallConfigsController.mainRecordMetters)
        {
            BallConfigsController.mainRecordMetters = BallConfigsController.currentMaxMetters;
        }

        if (BallConfigsController.currentMaxMetters == 0)
        {
            recordMettersText.text = "0m";
        }
        else
        {
            recordMettersText.text = BallConfigsController.mainRecordMetters.ToString("#m");
        }

        StartCoroutine(lerpCurrentMettersResult());
    }

    private void Update()
    {
        currentMettersText.text = currentMettersValueLerp.ToString("#m");
    }

    private IEnumerator lerpCurrentMettersResult()
    {
        while (currentMettersValueLerp != BallConfigsController.currentMaxMetters)
        {
            currentMettersValueLerp = Mathf.Lerp(currentMettersValueLerp, BallConfigsController.currentMaxMetters, 0.3f);
            yield return null;
        }
    }

}
