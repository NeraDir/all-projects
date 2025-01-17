using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class exempleAnswerComponent : MonoBehaviour
{
    public int answerValue;

    public TMP_Text valuetxt;

    private void LateUpdate()
    {
        transform.position += new Vector3(-1, 0,0) * coptercontrolling.speedOfAnswers * Time.deltaTime;
    }

    public void Show() 
    {
        valuetxt.text = answerValue.ToString();
    }
}
