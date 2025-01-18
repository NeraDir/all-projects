using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Transform _directionJumpVisual;
    [SerializeField] private RectTransform _JoyStickRect;
    [SerializeField] private RectTransform _JoyStickRectMushroom;
    [SerializeField] private CharacterMoving _characterMoving;
    [SerializeField] private float _MaxDistMous = 50;

    Vector3 poseClik = Vector2.zero;
    Vector3 poseMouse;
    bool dawn = false;
    public static CharacterController instance;
    private void Awake()
    {
        _JoyStickRect.gameObject.SetActive(false);
        _directionJumpVisual.gameObject.SetActive(false);
        if (instance != null)
        {
            Debug.Log("CharacterController > 1 on the scene");
            enabled = false;
            return;
        }
        instance = this;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            poseClik = Input.mousePosition;
            dawn = true;

            _JoyStickRect.gameObject.SetActive(true);
            _directionJumpVisual.gameObject.SetActive(true);
            _JoyStickRect.position = poseClik;
        }
        if (Input.GetMouseButtonUp(0) && dawn)
        {
            _JoyStickRect.gameObject.SetActive(false);
            _directionJumpVisual.gameObject.SetActive(false);

            dawn = false;

            poseMouse = Input.mousePosition;
            Vector2 dir = poseClik - poseMouse;

            if (Vector2.Distance(poseMouse, poseClik) < _MaxDistMous / 2 || !_characterMoving.waitForJump)
                return;

            _characterMoving.Jump(dir.normalized);
            
            poseClik = Vector2.zero;
            
        }
        if (!dawn)
            return;

        poseMouse = Input.mousePosition;

        Vector3 difference = poseClik - poseMouse;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        _directionJumpVisual.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);

        Vector3 delta = poseMouse - poseClik;
        delta = Vector2.ClampMagnitude(delta, _MaxDistMous);

        _JoyStickRectMushroom.position = _JoyStickRect.position + delta;
    }
}
