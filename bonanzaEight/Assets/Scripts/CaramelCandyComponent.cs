using Castle.Core.Internal;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaramelCandyComponent : MonoBehaviour
{
    private Joystick _joystick;

    private bool _isUpped;

    public bool isTriggered;

    [SerializeField]
    private Image _caramelPrefab;

    [SerializeField]
    private CaramelDatas _caramelDatas;


    private Sprite _mySprite;

    private bool _canTrigger;

    private void Start()
    {
        _mySprite = GetComponent<Image>().sprite;
        transform.localScale = Vector3.zero;
        float rndScale = Random.Range(0.4f, 0.9f);
        transform.DOScale(new Vector3(rndScale, rndScale, rndScale),0.15f);
        if (_joystick == null)
        {
            _isUpped = true;
            gameObject.AddComponent<Rigidbody2D>();
        }
        Invoke(nameof(Canned), 1);
    }

    private void Canned()
    {
        _canTrigger = true;
    }

    public Sprite GetMySprite()
    {
        return _mySprite;
    }

    public void Init(Joystick js)
    {
        _joystick = js;
    }

    private void LateUpdate()
    {
        if (CaramelTreatsGameController.go)
            return;
        if (_joystick == null)
            return;
        if (_isUpped)
            return;
        transform.position += new Vector3(_joystick.Horizontal, 0, 0) * 4 * Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {

        }
        else if (Input.GetMouseButtonUp(0))
        {
            gameObject.AddComponent<Rigidbody2D>();
            CaramelTreatsGameController.onUpMouse?.Invoke();
            CaramelTreatsGameController.onUpAddToList?.Invoke(gameObject);
            _isUpped = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CaramelCandyComponent candie))
        {
            if (!_canTrigger)
                return;
            if (_mySprite != candie.GetMySprite())
                return;
            if (isTriggered)
                return;
            _canTrigger = false;
            candie.DestroyMe(false);
            DestroyMe(true);
        }
    }

    private void DestroyMe(bool active)
    {
        isTriggered = true;
        if (!active)
        {
            CaramelTreatsGameController.caramelsInJar.Remove(gameObject);
            Destroy(gameObject);

        }

        transform.DOScale(Vector3.zero, 0.15f).OnComplete(() =>
        {
            if (active)
            {
                if (_mySprite == CaramelTreatsGameController.targetCaramel)
                {
                    Destroy(gameObject);
                }
                else
                {
                    int index = _caramelDatas.caramelSprites.IndexOf(_mySprite);
                    CaramelTreatsGameController.onSpawnNewCaramel?.Invoke(transform.position, index);
                    Destroy(gameObject);
                }
            }
            else
            {
                CaramelTreatsGameController.caramelsInJar.Remove(gameObject);
                Destroy(gameObject);

            }
        });
    }
}
