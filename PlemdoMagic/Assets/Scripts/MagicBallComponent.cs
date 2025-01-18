using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallComponent : MonoBehaviour
{
    public MagicBallPieceComponent[] magicPieces;

    public float magicBallAccuracy;

    public Rigidbody magicBody;

    private bool isFaller;

    public void Faller() 
    {
        if (isFaller)
            return;
        this.gameObject.AddComponent<Rigidbody>().constraints = magicBody.constraints;
        this.gameObject.AddComponent<SphereCollider>().radius = 1;
        isFaller = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MagicXPlaceComponente magicX))
        {
            MagicGameManager.Magicscore += Random.Range(5,10) *  magicX.valueX;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => { MagicGameManager.magicBallsList.Remove(this.gameObject); Destroy(gameObject); });
        }
    }

    private void LateUpdate()
    {
        if (isFaller)
            return;
        float value = 0;
        foreach (MagicBallPieceComponent piece in magicPieces)
            value += piece.accuracy;

        magicBallAccuracy = value;

       MagicGameManager.magicAccuracyTxt.text = magicBallAccuracy.ToString("0.0") + "%";
    }
}
