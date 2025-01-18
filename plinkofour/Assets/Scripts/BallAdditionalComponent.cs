using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallAdditionalComponent : MonoBehaviour
{
    private Sequence idleMove;

    public List<Material> ballMaterials;

    public bool canTrigger = true;

    private void Start()
    {
        if (Random.Range(0,2) != 0)
        {
            Destroy(gameObject);
        }
        GetComponent<MeshRenderer>().material = ballMaterials[Random.Range(0, ballMaterials.Count)];
    }

    public void StopIdleMove()
    {
        canTrigger = false;
        if (idleMove != null)
            idleMove.Kill();
        transform.parent = null;
    }
}
