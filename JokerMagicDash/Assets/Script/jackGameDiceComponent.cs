using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class jackGameDiceComponent : MonoBehaviour
{
    [SerializeField]
    private Vector3[] rotateVariants;

    [SerializeField]
    private int[] rotateValuesVariants;

    [SerializeField]
    private GameObject fallEffect;

    [SerializeField]
    private Transform moveToPosition;

    public int rotateValues;

    private float yDefaultPosition;

    private Vector3 startPosition;

    public bool isEnemie;

    private bool rotateble;

    public static UnityEvent isLastPlaced = new UnityEvent();

    public bool isLast;

    private void Start()
    {
        startPosition = transform.position;
        yDefaultPosition = transform.position.y;  
    }

    private void LateUpdate()
    {
        if (rotateble)
            transform.Rotate(new Vector3(1, 1, 0.5f), 360 * Time.deltaTime);
    }

    public void Launch() 
    {
        rotateble = true;
        transform.DOMoveX(moveToPosition.position.x, 2).OnComplete(() => rotateble = false);
        transform.DOMoveY(transform.position.y + 5, 1).OnComplete(() => transform.DOMoveY(yDefaultPosition,1).OnComplete(()=>SetRotate()));
        
    }

    public void ReLaunch(bool goBack) 
    {
        rotateble = true;
        transform.DOMoveX(startPosition.x, 2).OnComplete(() => rotateble = false);
        transform.DOMoveY(transform.position.y + 5, 1).OnComplete(() => transform.DOMoveY(yDefaultPosition, 1).OnComplete(() => { if(goBack == true) Invoke(nameof(Launch), 0.5f); }));
    }

    public void SetRotate()
    {
        int rndValue = Random.Range(0, rotateVariants.Length);
        transform.DORotateQuaternion(Quaternion.Euler(rotateVariants[rndValue]), 0.25f).OnComplete(() => Instantiate(fallEffect, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z), Quaternion.identity));
        rotateValues = rotateValuesVariants[rndValue];
        if (!isEnemie)
            jackGameManager.temper += rotateValues;
        else
        { 
            jackGameManager.Enemietemper += rotateValues;
            if (isLast)
            {
                isLastPlaced?.Invoke();
            }
        }
    }
}
