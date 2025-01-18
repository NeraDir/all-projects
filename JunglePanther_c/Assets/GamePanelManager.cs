using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GamePanelManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{

    [SerializeField]
    private float deathZone;

    [SerializeField]
    private TMP_Text coinDisplayTXT;
    [SerializeField]
    private TMP_Text mettersDisplayTXT;


    [SerializeField]
    private GameObject pausePanel;


    private Vector2 startSwipePos;
    private Vector2 swipeDelta;

    private SwipeType side;


    public static int gameCoinCount;

    public delegate void SwipeRead(SwipeType _side);
    public static event SwipeRead SwipeFixed;


    public void OnPointerDown(PointerEventData eventData)
    {
        startSwipePos = eventData.position;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        swipeDelta = eventData.position - startSwipePos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (swipeDelta.magnitude > deathZone)
        {

            if (swipeDelta.x > startSwipePos.x)
            {
                side = SwipeType.Right;
            }
            else
            {
                side = SwipeType.Left;
            }


            if (SwipeFixed != null)
                SwipeFixed(side);

            Debug.Log(side.ToString());
        }



        startSwipePos = swipeDelta = Vector2.zero;
    }

    private void OnEnable()
    {
        gameCoinCount = 0;
    }

    private void Update()
    {
        coinDisplayTXT.text = gameCoinCount.ToString();
        mettersDisplayTXT.text = MovementManager.playerZpos.ToString("#m");
    }

    public void ClickPausePage()
    {
        pausePanel.SetActive(true);
        gameObject.SetActive(false);
    }

}
