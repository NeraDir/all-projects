using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class UI_LevelCompletedPage : MonoBehaviour
{
    [SerializeField]
    private TMP_Text rewardCoinsText;

    private int rewardCoins;
    private float rewardCoinsLerp;


    private void OnEnable()
    {

        rewardCoins = (GamePlayConfigs.levelNumber * Random.Range(5, 11)) + Random.Range(50,101); 
        rewardCoinsLerp = 0;
        StartCoroutine(lerpReward());
    }


    private IEnumerator lerpReward()
    {
        while (rewardCoins != rewardCoinsLerp)
        {
            rewardCoinsLerp = Mathf.Lerp(rewardCoinsLerp, rewardCoins, 0.3f);
            rewardCoinsText.text = "+" + rewardCoinsLerp.ToString("#");
            yield return null;
        }
    }

    public void TapNextLevelButton()
    {
        GamePlayConfigs.coinsCount += rewardCoins;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void TapMenuButton()
    {
        GamePlayConfigs.coinsCount += rewardCoins;
        SceneManager.LoadScene("scenes_menu");

    }




}
