using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeastEnergySwipeManager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerMoveHandler
{
    private const float DEADZONE = 10.0f;

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeDelta, startTouch;

    [SerializeField] private Rigidbody[] _beastEnergyPlayerTransform;

    [SerializeField] private BeastEnergyPlayerControllerManager[] _beastEnergyPlayerControllerManager;

    private bool _beastEnergyReady = false;

    private void Start()
    {
        _beastEnergyReady = true;
    }

    private void Update()
    {
        if (!BeastEnergyGameManager.beastEnergyRunLaunched)
            return;
        // Rest all bool
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        // DeadZone
        if (swipeDelta.magnitude > DEADZONE)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    if (_beastEnergyPlayerTransform[BeastEnergyGameManager.beastCurrentSkinIndex].transform.position.x - 8.93f < -9f)
                        return;
                    if (!_beastEnergyReady)
                        return;
                    _beastEnergyPlayerTransform[BeastEnergyGameManager.beastCurrentSkinIndex].transform.DOMoveX(_beastEnergyPlayerTransform[BeastEnergyGameManager.beastCurrentSkinIndex].transform.position.x - 8.93f, 0.25f).OnComplete(() => _beastEnergyReady = true) ;
                }
                else
                {
                    if (_beastEnergyPlayerTransform[BeastEnergyGameManager.beastCurrentSkinIndex].transform.position.x + 8.93f > 9f)
                        return;
                    if (!_beastEnergyReady)
                        return;
                    _beastEnergyPlayerTransform[BeastEnergyGameManager.beastCurrentSkinIndex].transform.DOMoveX(_beastEnergyPlayerTransform[BeastEnergyGameManager.beastCurrentSkinIndex].transform.position.x + 8.93f, 0.25f).OnComplete(() => _beastEnergyReady = true);
                }
            }
            else
            {
                if (y < 0)
                {

                    _beastEnergyPlayerTransform[BeastEnergyGameManager.beastCurrentSkinIndex].AddForce(Vector3.down * 5,ForceMode.Impulse);
                    _beastEnergyPlayerControllerManager[BeastEnergyGameManager.beastCurrentSkinIndex].SetBeastAnimationState(3, true);
                }
                else
                {
                    if (!_beastEnergyPlayerControllerManager[BeastEnergyGameManager.beastCurrentSkinIndex].GetState())
                        return;
                    _beastEnergyPlayerTransform[BeastEnergyGameManager.beastCurrentSkinIndex].AddForce(Vector3.up *25, ForceMode.Impulse);
                    _beastEnergyPlayerControllerManager[BeastEnergyGameManager.beastCurrentSkinIndex].SetBeastAnimationState(2,true);
                }
            }

            startTouch = swipeDelta = Vector2.zero;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        startTouch = swipeDelta = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        tap = true;
        startTouch = Input.mousePosition;
    }

    public void OnPointerMove(PointerEventData eventData)
    {

        swipeDelta = Vector2.zero;
        if (startTouch != Vector2.zero)
        {
            swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }
    }
}
