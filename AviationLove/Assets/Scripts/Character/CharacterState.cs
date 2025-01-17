using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField]
    private GameObject[] jatpacks;

    public static int jatpackSelectedIndex 
    {
        get 
        {
            if (PlayerPrefs.HasKey("jatpackSelecetedndexSave"))
                return PlayerPrefs.GetInt("jatpackSelecetedndexSave");
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("jatpackSelecetedndexSave", value);
        }
    }

    private void Start()
    {
        jatpacks[jatpackSelectedIndex].SetActive(true);
    }
}
