using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettysMoveBlock : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, 1, 0) * (60) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BettysBlockComponent block))
        {
            meshRenderer.material = block.material;
            transform.gameObject.layer = block.gameObject.layer;
            transform.parent = block.transform.parent;
            gameObject.AddComponent<BettysBlockComponent>();
            Destroy(this);
        }
    }
}
