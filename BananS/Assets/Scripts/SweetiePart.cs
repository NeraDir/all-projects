using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SweetiePart : MonoBehaviour
{
    private Sequence idleMove;
    private Transform sTransform;

    public List<Material> sweetieMaterials;

    public bool canTrigger = true;

    private void OnEnable()
    {
        sTransform = GetComponent<Transform>();

        
        idleMove = DOTween.Sequence();
        idleMove.Append(sTransform.DOLocalMoveY(1, 2f));
        idleMove.Append(sTransform.DOLocalMoveY(0, 2f));
        idleMove.SetLoops(-1 ,LoopType.Yoyo);
        //
        
       
        //meshRenderer.materials

        //sTransform.DOLocalMoveY(5, 2f).OnComplete(()=> sTransform.DOLocalMoveY(0, 2f));

    }

    private void Start()
    {
        //GetComponent<MeshRenderer>().materials.Se
        GetComponent<MeshRenderer>().material = sweetieMaterials[Random.Range(0, sweetieMaterials.Count)];
    }

    public void StopIdleMove()
    {
        canTrigger = false;

        if(idleMove != null)
            idleMove.Kill();

        transform.parent = null;
    }
}
