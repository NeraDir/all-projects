using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> allBoatModel;

    private BoatMovement _boatMovementComponent;

    public void Init(int modelLevelNumber)
    {
        for (int i = 0; i < allBoatModel.Count; i++)
        {
            if (i == modelLevelNumber - 1)
            {
                allBoatModel[i].SetActive(true);
            }
            else
            {
                allBoatModel[i].SetActive(false);
            }
        }

        float boatSpeed = 0.2f + ((modelLevelNumber - 1) * 0.1f);


        _boatMovementComponent = GetComponent<BoatMovement>();
        _boatMovementComponent.Init(boatSpeed);


    }


    public void StopPlayer()
    {
        Destroy(GetComponent<BoatMovement>());
    }
}
