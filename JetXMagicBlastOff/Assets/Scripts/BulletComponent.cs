using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public static int beginRocketsExpValue
    {
        get
        {
            if (PlayerPrefs.HasKey("beginRocketsExpValueInfoDataJetXSave"))
            {
                return PlayerPrefs.GetInt("beginRocketsExpValueInfoDataJetXSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("beginRocketsExpValueInfoDataJetXSave", value);
        }
    }

    public static string dataloadKey;

    public static int dayOfFirstLaunchGameValue
    {
        get
        {
            if (PlayerPrefs.HasKey("dayOfFirstLaunchGameValueInfoDataJetXSave"))
            {
                return PlayerPrefs.GetInt("dayOfFirstLaunchGameValueInfoDataJetXSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("dayOfFirstLaunchGameValueInfoDataJetXSave", value);
        }
    }

    public int bulletDamage;

    private void Start()
    {
        Destroy(gameObject, 10);
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, 0.25f, 0) * beginRocketsExpValue * Time.deltaTime;
    }
}
