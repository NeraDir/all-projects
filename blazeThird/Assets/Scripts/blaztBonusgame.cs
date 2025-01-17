using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class blaztBonusgame : MonoBehaviour
{
    private float speed;

    private bool _isClicked;

    [SerializeField]
    private Transform _spinItTarget;

    [SerializeField]
    private GameObject _spinItButton;

    public static int currentMulti;

    [SerializeField]
    private TMP_Text[] _displayCurrentMulti;

    private void Start()
    {
        currentMulti = 1;
    }

    private void LateUpdate()
    {
        foreach (var item in _displayCurrentMulti)
        {
            item.text = "x" + currentMulti.ToString();
        }
        
        if (!_isClicked)
            return;
        speed -= 100 * Time.deltaTime;
        if (speed <= 0)
        {
            speed = 0;
        }
        _spinItTarget.Rotate(new Vector3(0, 0, 1), speed * Time.deltaTime);
    }

    private IEnumerator Waiting()
    {
        while (speed > 0)
        {
            if (speed <= 0)
            {
                speed = 0;
            }
            yield return null;
        }
        
        yield return new WaitForSeconds(2);
        blaztGame.currentScore = blaztGame.currentScore * currentMulti;
        gameObject.SetActive(false);
    }

    public void OnClickSpinIt()
    {
        if (_isClicked)
            return;
        _isClicked = true;
        speed = 1400;
        _spinItButton.SetActive(false);
        StartCoroutine(Waiting());
    }
}
