using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StalkPlaneComponent : MonoBehaviour
{
    public StalkPlanePieces[] stalkPieces;

    public ParticleSystem particleSystem;

    public Transform planePlace;

    public Transform planeEndPlace;

    public Transform planeStartPlace;

    public PlaneMeshes[] planeMeshes;

    public MeshFilter[] planeFilter;

    public Vector3 planeDeliveryPlanePosition;

    public Transform planeDeliveryDesk;

    public GameObject[] planeRecoverDispalyerStatus;

    private void OnEnable()
    {
        particleSystem.startLifetime = 2.36f;
        transform.localPosition = planeStartPlace.localPosition;
        transform.localRotation = planeStartPlace.localRotation;
        planeDeliveryDesk.gameObject.SetActive(true);
        planeDeliveryPlanePosition = planeDeliveryDesk.localPosition;
        int rndPlaneMesh = Random.Range(0, planeMeshes.Length);
        for (int i = 0; i < planeFilter.Length; i++)
        {
            planeFilter[i].mesh = planeMeshes[rndPlaneMesh].planeMeshes[i];
        }
        foreach (var item in stalkPieces)
        {
            if (item.workersStartPositions.Count > 0)
            {
                for (int i = 0; i < item.workersStartPositions.Count; i++)
                {
                    item.PlaneWorkers[i].transform.localPosition = item.workersStartPositions[i];
                }
                item.PlanePiece.transform.localPosition = item.pieceStartPos;
            }
            if (item.recovered)
            {
                item.PlanePiece.localPosition = Vector3.zero;
                item.PlanePiece.gameObject.SetActive(true);
                StalkGamingManager.currentplaneHealth += 1;
                item.pieceButton.SetActive(false);
                particleSystem.startLifetime -= 0.922f;
                foreach (var item3 in item.PlaneWorkers)
                {
                    item3.gameObject.SetActive(false);
                }
            }
            else
            {
                item.pieceButton.SetActive(true);
                foreach (var item3 in item.PlaneWorkers)
                {
                    item3.gameObject.SetActive(true);
                }
            }
        }
        FindObjectOfType<StalkGamingManager>().recovererSlider.gameObject.SetActive(false);

        transform.DOLocalMove(planePlace.localPosition, 1.5f);
    }

    public void MoveSelectedPiece(int index, float posValue)
    {
        stalkPieces[index].PlanePiece.gameObject.SetActive(true);
        stalkPieces[index].workersStartPositions.Clear();
        foreach (var item in stalkPieces[index].PlaneWorkers)
        {
            stalkPieces[index].workersStartPositions.Add(item.transform.localPosition);
        }
        stalkPieces[index].pieceStartPos = stalkPieces[index].PlanePiece.transform.localPosition;
        stalkPieces[index].PlanePiece.DOLocalMoveZ(posValue, 1.5f).OnComplete(() =>
        {
            foreach (var item in stalkPieces[index].PlaneWorkers)
            {
                item.transform.DOLocalMoveZ(item.transform.position.z + 10, 4).OnComplete(() => 
                {
                    item.gameObject.SetActive(false);
                    item.SetBool("worker", true);
                    StalkGamingManager.cangSelectNewPiece = false;
                });

            }
            FindObjectOfType<StalkGamingManager>().recovererButtons[index].SetActive(false);
            switch (StalkGamingManager.placeState)
            {
                case "Normal":
                    particleSystem.startLifetime -= 0.322f;
                    planeRecoverDispalyerStatus[2].SetActive(true);
                    StalkGamingManager.currentplaneHealth += 0.322f;
                    break;
                case "Good":
                    particleSystem.startLifetime -= 0.622f;
                    planeRecoverDispalyerStatus[1].SetActive(true);
                    StalkGamingManager.currentplaneHealth += 0.622f;
                    break;
                case "Amazing":
                    particleSystem.startLifetime -= 0.922f;
                    planeRecoverDispalyerStatus[0].SetActive(true);
                    StalkGamingManager.currentplaneHealth += 1;
                    break;
                case "Break":
                    planeRecoverDispalyerStatus[3].SetActive(true);
                    break;
            }
            stalkPieces[index].pieceButton.SetActive(false);
            FindObjectOfType<StalkGamingManager>().recovererSlider.gameObject.SetActive(false);

            stalkPieces[index].recovered = true;
            if (!stalkPieces[0].recovered)
            {
                return;
            }
            if (!stalkPieces[1].recovered)
            {
                return;
            }
            if (!stalkPieces[2].recovered)
            {
                return;
            }
            planeDeliveryDesk.DOLocalMove(planeDeliveryPlanePosition, 3).OnComplete(() => planeDeliveryDesk.gameObject.SetActive(false));
            Invoke("Go", 4);
        });
    }

    private void Go() 
    {
        transform.DOLocalRotateQuaternion(planeEndPlace.localRotation, 2);
        transform.DOLocalMove(planeEndPlace.localPosition, 3).OnComplete(() =>
        {
            gameObject.SetActive(false); FindObjectOfType<StalkGamingManager>().GetNewPlane(); StalkGamingManager.sliderMoveValue += 0.25f; 
            if (StalkGamingManager.currentplaneHealth > 3)
            {
               StalkGamingManager.recoveredPlanesCount++;
            }
            else
            {
                StalkGamingManager.stalkPlayerHearts--;
            }
        });
    }
}


[System.Serializable]
public class PlaneMeshes 
{
    public Mesh[] planeMeshes;
}

[System.Serializable]
public class StalkPlanePieces
{
    public Transform PlanePiece;

    public Animator[] PlaneWorkers;

    public Transform[] positions;

    public List<Vector3> workersStartPositions = new List<Vector3>();

    public Vector3 pieceStartPos;

    public GameObject pieceButton;

    public bool recovered;
}
