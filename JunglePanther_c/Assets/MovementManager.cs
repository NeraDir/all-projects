using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField]
    private float spacingBetweenLines;

    private PositionState currentPositionState;

    [SerializeField]
    private float forwardSpeed;

    private Vector3 curerentPosition;
    private float currentXpos;
    private float currentXposLerp;


    private float currentZpos;
    private Transform mTransform;

    public static float playerZpos;

    [SerializeField]
    private List<GameObject> allModels;


    private void OnEnable()
    {
        GamePanelManager.SwipeFixed += SwitchLine;
    }
    private void OnDisable()
    {
        GamePanelManager.SwipeFixed -= SwitchLine;
    }



    private void Start()
    {
        currentPositionState = PositionState.Midle;

        int currentModelIndex = PantherRunnerData.modelIndex;

        mTransform = GetComponent<Transform>();

        for (int i = 0; i < allModels.Count; i++)
        {
            if (i == currentModelIndex)
            {
                allModels[i].SetActive(true);
            }
            else
            {
                allModels[i].SetActive(false);
            }
        }

        forwardSpeed += (0.01f * PantherRunnerData.modelIndex);
    }

    private void FixedUpdate()
    {





        currentXposLerp = Mathf.Lerp(currentXposLerp, currentXpos, 0.3f);

        mTransform.position = new Vector3(currentXposLerp, mTransform.position.y, mTransform.position.z);
        mTransform.position += mTransform.forward * forwardSpeed;
        playerZpos = mTransform.position.z;
    }

    private void SwitchLine(SwipeType swipeType)
    {
        if (currentPositionState == PositionState.Left)
        {
            if (swipeType == SwipeType.Left)
            {
                return;
            }
            else
            {
                currentXpos += spacingBetweenLines;
                currentPositionState = PositionState.Midle;
            }

        }
        else if (currentPositionState == PositionState.Midle)
        {
            if (swipeType == SwipeType.Left)
            {
                currentXpos -= spacingBetweenLines;
                currentPositionState = PositionState.Left;
            }
            else if (swipeType == SwipeType.Right)
            {
                currentXpos += spacingBetweenLines;
                currentPositionState = PositionState.Right;
            }

        }
        else if(currentPositionState == PositionState.Right)
        {
            if (swipeType == SwipeType.Left)
            {
                currentXpos -= spacingBetweenLines;
                currentPositionState = PositionState.Midle;
            }
            else
            {
                return;
            }

        }
    }
}

public enum PositionState
{
    Left,
    Midle,
    Right
}

public enum SwipeType
{
    Left,
    Right
}