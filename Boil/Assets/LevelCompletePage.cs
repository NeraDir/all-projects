using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCompletePage : MonoBehaviour
{
    public List<GameObject> stars;
    public TMP_Text collectedCoinsDisplayText;




    private void OnEnable()
    {
        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].SetActive(false);
        }


        int starsCount = 0;
        float starsPer = LevelHandler.levelCoinsCount / LevelHandler.maxCoinsCount;

        if(starsPer < 0.2)
        {
            starsCount = 1;
        }
        else if (starsPer < 1)
        {
            starsCount = 2;
        }
        else
        {
            starsCount = 3;
        }


        if(LevelHandler.levelData.starsCount < starsCount)
        {
            //Debug.Log(LevelHandler.levelData.levelKey);
            PlayerPrefs.SetInt(LevelHandler.levelData.levelKey, starsCount);
        }


        StartCoroutine(showStarts(starsCount));

        collectedCoinsDisplayText.text = LevelHandler.levelCoinsCount.ToString("+#");
    }


    private IEnumerator showStarts(int count)
    {

        yield return new WaitForSeconds(1f);

        for(int i = 0; i < count; i++)
        {
            stars[i].SetActive(true);
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
}
