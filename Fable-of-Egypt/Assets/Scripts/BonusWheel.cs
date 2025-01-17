using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Analytics;
using System;
using Random = UnityEngine.Random;

public class BonusWheel : MonoBehaviour
{
    [SerializeField]
    private Image[] _images;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Sprite[] _icons;

    private bool _isRotate;

    [SerializeField]
    private TMP_Text _textWon;

    [SerializeField]
    private Wallet _wallet;

    private bool _ended;

    private void OnEnable()
    {
        _isRotate = false;
        _animator.enabled = false;
        _ended = false;
        _textWon.text = "";
    }

    public void onClickRotate() 
    {
        if (_isRotate && _ended)
            return;
        _animator.enabled = false;
        _isRotate = true;
        _ended = true;
        Invoke(nameof(SetIcons), 0.3f);
        StartCoroutine(Rotating());
    }

    private void SetIcons() 
    {
        for (int i = 0; i < _images.Length; i++)
        {
            _images[i].sprite = _icons[Random.Range(0, _icons.Length)];
        }
    }

    public void EndAnima()
    {
        _isRotate = false;
    }

    private IEnumerator Rotating() 
    {
        yield return new WaitForSeconds(0.1f);
        while (_isRotate)
        {
            _animator.enabled = true;
            yield return null;
        }
        _animator.enabled = false;
        _textWon.text = "YOU WON: " + (Random.Range(10, 30)).ToString("0");
        Wallet.instance.AddCoin(Convert.ToInt32((_textWon.text).Replace("YOU WON: ","")));
        _isRotate = true;
        StartCoroutine(Closing());
    }

    private IEnumerator Closing() 
    {
        yield return new WaitForSeconds(2);
        _isRotate = false;
        gameObject.transform.parent.gameObject.SetActive(false);
        _ended = false;
        StopAllCoroutines();
    }
}
