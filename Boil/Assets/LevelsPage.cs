using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelsPage : MonoBehaviour
{
    public List<LevelData> levelDatas;
    public List<LevelPanel> levelPanels;

    private Animator mAnimator;

    public MenuPage menuPage;

    private void Init()
    {
        for (int i = 0; i < levelDatas.Count; i++)
        {
            int startsCount = 0;

            if (!PlayerPrefs.HasKey(levelDatas[i].levelKey))
            {
                startsCount = 0;
                PlayerPrefs.SetInt(levelDatas[i].levelKey, 0);
            }
            else
            {
                startsCount = PlayerPrefs.GetInt(levelDatas[i].levelKey);
            }

            levelDatas[i].starsCount = startsCount;
            levelPanels[i].SetLevelData(levelDatas[i]);
        }
    }


    private void OnEnable()
    {
        Init();
    }

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
    }


    public void CloseMe()
    {
        PlayCloseAnimation();
        StartCoroutine(CloseAndOpenMenu());
    }


    public void PlayOpenAnimation()
    {
        mAnimator.SetInteger("key", 0);
    }
    public void PlayCloseAnimation()
    {
        mAnimator.SetInteger("key", 1);
    }


    private IEnumerator CloseAndOpenMenu()
    {

        float waitTime = mAnimator.runtimeAnimatorController.animationClips[1].length;


        yield return new WaitForSeconds(waitTime);

        gameObject.SetActive(false);
        menuPage.gameObject.SetActive(true);
    }
}
[Serializable]
public class LevelData
{
    public string levelKey;
    public int number;
    public int starsCount;

    public string sceneKey;
}
