using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 20f;

    bool isAttacked = false;
    RectTransform TargetRect;
    RectTransform FromRect;
    public RectTransform MyRect;

    private void Update()
    {
        if(isAttacked && TargetRect != null)
        {
            //MyRect.LookAt(TargetRect.position);
            MyRect.eulerAngles = FromRect.eulerAngles;
            MyRect.position = Vector3.MoveTowards(MyRect.position, TargetRect.position, Speed * Time.deltaTime);

            if(MyRect.position == TargetRect.position)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SwitchOnAttack(EnemySTR _targetSTR)
    {
        MyRect.position = _targetSTR.FromPos.position;
        FromRect = _targetSTR.FromPos;
        TargetRect = _targetSTR.TargetRect;
        isAttacked = true;
    }
}
