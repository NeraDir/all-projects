using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAndSounManager : MonoBehaviour
{
    [SerializeField] private GameObject mBut1;
    [SerializeField] private GameObject mText1;

    [SerializeField] private GameObject mBut2;
    [SerializeField] private GameObject mText2;


    [SerializeField] private GameObject sBut1;
    [SerializeField] private GameObject sText1;

    [SerializeField] private GameObject sBut2;
    [SerializeField] private GameObject sText2;

    [SerializeField] private AudioSource au;

    void Start()
    {
        if (PlayerPrefs.GetInt("music",1) == 1) 
        {
            staticInfo.music = true;
            au.mute = false;
            mBut1.SetActive(true);
            mText1.SetActive(true);

            mBut2.SetActive(false);
            mText2.SetActive(false);
        }
        else 
        {
            staticInfo.music = false;
            au.mute = true;
            mBut1.SetActive(false);
            mText1.SetActive(false);

            mBut2.SetActive(true);
            mText2.SetActive(true);
        }

        if (PlayerPrefs.GetInt("sound",1) == 1) 
        {
            staticInfo.sound = true;
            sBut1.SetActive(true);
            sText1.SetActive(true);

            sBut2.SetActive(false);
            sText2.SetActive(false);
        }
        else 
        {
            staticInfo.sound = false;
            sBut1.SetActive(false);
            sText1.SetActive(false);

            sBut2.SetActive(true);
            sText2.SetActive(true);
        }
    }

    public void MusicChange(bool wa) 
    {
        staticInfo.music = wa;
        if (wa) 
        {
            PlayerPrefs.SetInt("music",1);
        }
        else 
        {
           PlayerPrefs.SetInt("music",0);
        }
    }
    public void SoundChange(bool wa) 
    {
        staticInfo.sound = wa;
        if (wa) 
        {
            PlayerPrefs.SetInt("sound",1);
        }
        else 
        {
           PlayerPrefs.SetInt("sound",0);
        }
    }
}
