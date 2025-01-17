using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaramelCannonBonusGame : MonoBehaviour
{
    [SerializeField]
    private Text[] _caramelTxtX;

    [SerializeField]
    private GameObject _caramelSpinButton;

    [SerializeField]
    private Text _caramelResultXTxt;

    public static Text winTxt;

    private void Start()
    {
        foreach (var item in _caramelTxtX)
        {
            item.text = "x" + UnityEngine.Random.Range(1,10).ToString("0");
        }
    }

    private void LateUpdate()
    {
        if (winTxt != null) 
            _caramelResultXTxt.text = winTxt.text;
    }

    public void OnClickCaramelSpin() 
    {
        _caramelSpinButton.SetActive(false);
        StartCoroutine(Controler());
    }

    private IEnumerator Controler()
    {
        int rotateion = 0;
        while (rotateion < 5)
        {
            transform.Rotate(new Vector3(0, 0, 1), 720* Time.deltaTime);
            if (transform.eulerAngles.z >=350)
            {
                rotateion++;
            }
            yield return null;
        }
        CaramelCanonGameManager.caramelStarsPerSession *= int.Parse(winTxt.text.Replace("x",""));
        yield return new WaitForSeconds(3);
        transform.parent.gameObject.SetActive(false);
    }
}
