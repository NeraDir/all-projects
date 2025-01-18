using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameCompoentn : MonoBehaviour
{
    public static int BestRecord
    {
        get
        {
            if (PlayerPrefs.HasKey("PortalSpheresBestReacordKey"))
            {
                return PlayerPrefs.GetInt("PortalSpheresBestReacordKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PortalSpheresBestReacordKey", value);
        }
    }

    public static int portalSphereTryCount
    {
        get
        {
            if (PlayerPrefs.HasKey("portalSphereTryCountdsidfguisdgasaves"))
            {
                return PlayerPrefs.GetInt("portalSphereTryCountdsidfguisdgasaves");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("portalSphereTryCountdsidfguisdgasaves", value);
        }
    }

    public static string portalSphereName;

    public static int portalSphereWinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("portalSphereWinsCountdsaguudsgSave"))
            {
                return PlayerPrefs.GetInt("portalSphereWinsCountdsaguudsgSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("portalSphereWinsCountdsaguudsgSave", value);
        }
    }

    public static UnityEvent ballDead = new UnityEvent();

    public GameObject amazingScreen;

    private void Start()
    {
        ballDead.AddListener(OnBallDead);
    }

    private void OnBallDead()
    {
        MoveObjectComponent.speed = 0;
        amazingScreen.SetActive(true);
    }

    public void OnCLickButton(int index)
    {
        switch (index)
        {
            case 0:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case 1:
                SceneManager.LoadScene("SphereMenu");
                break;
        }
    }
}
