using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public static bool _isClicked;

    private bool _onWall;

    private Rigidbody2D _body2D;

    private Vector3 direction;

    private bool _firstClick;

    private int _healthvalue;

    public Transform[] _healthImages;

    private int _score;

    public TMP_Text[] scoreTxts;
    private bool _shieldIsActive;

    private float _shieldDuration;

    private float _currentShieldDuration;

    public Image _shieldImage;

    private void Start()
    {
        _score = 0;
        _healthvalue = 5;
        _body2D = GetComponent<Rigidbody2D>();  
        _isClicked = false;
        _firstClick = false;
        _shieldDuration = 5;
        _currentShieldDuration = 0;
    }

    private void OnTakeDamage()
    {
        if(_shieldIsActive)
        {
            return;
        }
        _healthvalue -= 1;
        for (int i = 0; i < _healthImages.Length; i++)
        {
            if (i >= _healthvalue)
            {
                _healthImages[i].DOScale(Vector3.zero, 0.25f);
            }
        }
        if (_healthvalue <=0)
        {
            GameCompoentn.ballDead?.Invoke();
        }
    }

    private void LateUpdate()
    {
        if (_score > GameCompoentn.BestRecord)
        {
            GameCompoentn.BestRecord = _score;
        }
        if (_shieldIsActive)
        {
            _shieldImage.gameObject.SetActive(true);
            _currentShieldDuration += Time.deltaTime;
            if (_currentShieldDuration >= _shieldDuration)
            {
                _shieldImage.gameObject.SetActive(false);
                _shieldIsActive = false;
                _currentShieldDuration = 0;
            }
        }
        if (_isClicked)
        {
            if (_onWall)
                return;
            transform.position += new Vector3(direction.x, direction.y, 0) * 2.5f * Time.deltaTime;
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            
            _isClicked = true;
            transform.DOMoveX(transform.localPosition.x > 0 ? transform.position.x - 30 : transform.position.x + 30, 0.1f).OnComplete(() => direction = Input.mousePosition - transform.position);
            if (_firstClick)
                return;
            _firstClick = true;
            MoveObjectComponent.speed = 300;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WallComponent wall))
        {
            direction = Vector3.zero;
            _body2D.velocity = Vector3.zero;
            transform.position = new Vector3(transform.localPosition.x > 0 ? transform.position.x - 30 : transform.position.x + 30, transform.position.y, 0);
            _onWall = true;
            CamController.moveObjectsToViewPos?.Invoke();
            _isClicked = false;
        }
        if (collision.TryGetComponent(out LineOfEndPartComponent lineEnd))
        {
            PartsSpawner.spawnPart?.Invoke();
        }
        if (collision.TryGetComponent(out SawComponent saw))
        {
            saw.transform.DOScale(Vector3.zero, 0.12f).OnComplete(() => Destroy(saw.gameObject));
            OnTakeDamage();
        }
        if (collision.TryGetComponent(out StarComponent star))
        {
            _score += 1;
            foreach (var item in scoreTxts)
            {
                item.text = _score.ToString();
            }
            star.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(star.gameObject));
        }
        if (collision.TryGetComponent(out DeadLineComponent deaLine))
        {
            GameCompoentn.ballDead?.Invoke();
        }
        if (collision.TryGetComponent(out ShieldComponente shield))
        {
            shield.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(shield.gameObject));
            _shieldIsActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _onWall = false;
    }
}
