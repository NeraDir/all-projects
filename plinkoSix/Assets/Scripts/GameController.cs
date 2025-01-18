using NSubstitute.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static int pinoSorceyTryCounter
    {
        get
        {
            if (PlayerPrefs.HasKey("pinoSorceyTryCounterSisdigsudgudfhds"))
            {
                return PlayerPrefs.GetInt("pinoSorceyTryCounterSisdigsudgudfhds");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("pinoSorceyTryCounterSisdigsudgudfhds", value);
        }
    }

    public static string pinoSorceyNames;

    public static int pinoWinsCounter
    {
        get
        {
            if (PlayerPrefs.HasKey("pinoWinsCounterWaninssdgsadgsd"))
            {
                return PlayerPrefs.GetInt("pinoWinsCounterWaninssdgsadgsd");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("pinoWinsCounterWaninssdgsadgsd", value);
        }
    }

    public Balls[] startBalls;
    public Transform[] holesTomake;
    public float radius;
    public TouchController cam;
    public List<Balls> activeBalls;
    public GameObject endScreen;
    public Animator animator;

    void Start()
    {
        foreach (Balls ball in startBalls)
        {
            ball.TransitionToState(ball.activeState);
        }

        foreach (Transform tf in holesTomake)
        {
            cam.defromToHoles(tf.position, radius);
        }
    }
}
