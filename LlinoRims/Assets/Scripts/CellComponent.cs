using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CellType
{
    Wall,
    Ball,
    Button,
    Saw,
    Door,
    Finish,
    Nothing
}

public class CellComponent : MonoBehaviour, IPointerClickHandler
{
   public CellType CellType;

   private bool _isClicked;

   private BallComponent _ballComponent;

   public void OnPointerClick(PointerEventData eventData)
   { 
       _ballComponent = FindObjectOfType<BallComponent>();
       if(CellType == CellType.Ball)
           return;
       if(CellType == CellType.Door)
           return;
       if(CellType == CellType.Saw)
           return;
       if(CellType == CellType.Wall)
           return;
       if(_ballComponent.GetBallState())
           return;
       if(_isClicked)
           return;
       if (Vector3.Distance(transform.position, _ballComponent.transform.position) > 0.6f) 
           return;
       _isClicked = true;
       BallComponent.moveTo?.Invoke(transform);
   }
}
