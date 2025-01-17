using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class GoldGameManagment : MonoBehaviour
{
    public GameObject[] goldBalls;

    public Transform goldBallsSpawnPosition;

    public GameObject[] goldfHeartsImages;

    public static int goldScore;

    public static int goldHeartsCount;

    public TMP_Text goldScoreTxt;

    public TMP_Text goldBallsEarnedCountTxt;

    public static int goldballsEarnedCount;

    public static List<GameObject> goldErnedballsList = new List<GameObject>();

    private float goldSpeed;

    public PostProcessVolume goldProccessVolume;

    private LensDistortion lens;

    private IEnumerator Start()
    {
        goldSpeed = 5;
        goldErnedballsList.Clear();
        goldHeartsCount = 3;
        goldballsEarnedCount = 0;
        goldScore = 0;
        int spaningCount = 0;
        lens = goldProccessVolume.profile.GetSetting<LensDistortion>();
        while (true)
        {
            int goldRndIndex = Random.Range(0, goldBalls.Length);
            GameObject goldBallTemp = Instantiate(goldBalls[goldRndIndex], goldBallsSpawnPosition.position, Quaternion.identity);
            goldBallTemp.GetComponent<GoldBall>().goldBallSpeed = goldSpeed;
            goldBallTemp.GetComponent<GoldBall>().goldPrefab = goldBalls[goldRndIndex];
            spaningCount++;
            if (spaningCount >= 3)
            {
                goldSpeed += 1;
                spaningCount = 0;
            }
            yield return new WaitForSeconds(2);
        }
    }

    private void LateUpdate()
    {
        goldScoreTxt.text = goldScore.ToString("0");
        goldBallsEarnedCountTxt.text = goldballsEarnedCount.ToString("0");
        for (int i = 0; i < goldfHeartsImages.Length; i++)
        {
            if (i >= goldHeartsCount)
            {
                goldfHeartsImages[i].transform.DOScale(Vector3.zero,0.5f);
            }
        }
        if (goldHeartsCount <= 0)
        {
            lens.intensity.value = Mathf.MoveTowards(lens.intensity.value, -100, 40 * Time.deltaTime);
            
            if (lens.intensity.value <= -100)
            {
                lens.scale.value = Mathf.MoveTowards(lens.intensity.value, 0, 10 * Time.deltaTime);
                if (lens.scale.value <= 0)
                {
                    SceneManager.LoadScene("GoldPostGame");
                }
            }
        }
    }
}
