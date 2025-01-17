using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWallTrapComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wallPieces;

    private int index = 0;

    [SerializeField]
    private Material[] wallColors;

    private BoxCollider collider;

    private void Start()
    {
        index = Random.Range(0, wallColors.Length);
        foreach (var piece in wallPieces) 
        {
            piece.GetComponent<MeshRenderer>().material = wallColors[index];
        }
    }

    public void DestroyMe(int index,Rigidbody body) 
    {
        if (this.index == index)
        {
            Destroy(collider);
            foreach (var piece in wallPieces)
            {
                piece.AddComponent<Rigidbody>();
            }
        }
        else
        {
            body.AddForce(new Vector3(-2,0,0) * 10, ForceMode.Impulse);
            GameController.ballHeartsCount--;
            Destroy(collider);
            foreach (var piece in wallPieces)
            {
                piece.AddComponent<Rigidbody>();
            }
        }
    }
}
