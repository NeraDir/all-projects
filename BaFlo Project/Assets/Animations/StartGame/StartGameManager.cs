using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{

    [SerializeField]
    private Animator playerAnimator;

    public delegate void StartGameDelegate();
    public static event StartGameDelegate StartGameEvent;

    private void Awake()
    {
        playerAnimator.enabled = false;
    }

    public void StartGame()
    {
        playerAnimator.enabled = true;

        if (StartGameEvent != null)
            StartGameEvent();
        Destroy(GetComponent<Animator>());
        Destroy(this);
    }
}
