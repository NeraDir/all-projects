using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MathComponent : MonoBehaviour
{
    public float value;

    private TMP_Text showThisValue;

    public int index;

    MathComponent matherCenter;

    private void Start()
    {
        foreach (var item in FindObjectsOfType<MathComponent>())
        {
            if (item.index == 1)
            {
                matherCenter = item;
                break;
            }
        }
    }

    public void Inuit()
    {
        showThisValue = GetComponent<TMP_Text>();
        showThisValue.text = value.ToString("0");
        
    }

    private void LateUpdate()
    {
        if (index == 1)
            transform.position += Vector3.down * 1 * Time.deltaTime;
        else
        {
            transform.RotateAround(matherCenter.transform.position,new Vector3(0,0,1),100 * Time.deltaTime);
            transform.position += Vector3.down * 0.5f * Time.deltaTime;
        }
    }
}
