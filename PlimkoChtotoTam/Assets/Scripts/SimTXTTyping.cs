using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimTXTTyping : MonoBehaviour
{
    private TMP_Text txt;
    [SerializeField]
    private string needTxt = "LOADING";

    [SerializeField]
    private float timeType;

    void Awake()
    {
        txt = GetComponent<TMP_Text>();
        needTxt = txt.text;
        txt.text = "";
        StartCoroutine(PlayText());
    }

    private IEnumerator PlayText()
    {
        while (true) 
        {
            foreach (char c in needTxt)
            {
                txt.text += c;
                
                yield return new WaitForSeconds(timeType);
            }
            if (txt.text == needTxt)
            {
                txt.text = "";
            }
            yield return null;
        }
        
    }
}
