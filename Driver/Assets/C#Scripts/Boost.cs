using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CarMoving cm = collision.GetComponent<CarMoving>();
        if (cm != null)
        {
            cm.Boost();
            Destroy(gameObject);
        }
    }

    public static int BoostValue
    {
        get
        {
            if (PlayerPrefs.HasKey("BoostValueSavekey"))
            {
                return PlayerPrefs.GetInt("BoostValueSavekey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("BoostValueSavekey", value);
        }
    }

    public static int BoostDurationValue
    {
        get
        {
            if (PlayerPrefs.HasKey("BoostDurationValueSaveKey"))
            {
                return PlayerPrefs.GetInt("BoostDurationValueSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("BoostDurationValueSaveKey", value);
        }
    }

}
