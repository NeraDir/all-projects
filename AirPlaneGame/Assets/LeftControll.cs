using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftControll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Player _player;
    [SerializeField] private GameManager _gm;
    [SerializeField] private bool _moving;
    public void OnPointerDown(PointerEventData eventData)
    {
        _moving = true;
        _gm.StartGame();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        _moving = false;
        _player.StopMoving();
    }

    private void Update() 
    {
        if (_moving) 
        {
            _player.MoveLeft(); 
        }
    }
}
