using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldPostGameController : MonoBehaviour
{
    public TMP_Text goldScoreShow;

    public TMP_Text goldResultScoreShow;

    public static int goldResultScore;

    public GameObject goldResultsScreen;

    public Transform[] spawnPositipons;

    private void Start()
    {
        goldResultScore = 0;

        StartCoroutine(SpawningBalls());
    }

    private IEnumerator SpawningBalls()
    {
        int spawningCount = GoldGameManagment.goldErnedballsList.Count;
        while (spawningCount > 0)
        {
            GameObject tempBall = Instantiate(GoldGameManagment.goldErnedballsList[spawningCount - 1], new Vector3(Random.Range(spawnPositipons[0].position.x, spawnPositipons[1].position.x), spawnPositipons[0].position.y, spawnPositipons[0].position.z), Quaternion.identity);
            tempBall.GetComponent<GoldBall>().goldBallGo = true;
            tempBall.transform.localScale = tempBall.transform.localScale / 1.5f;
            spawningCount--;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void LateUpdate()
    {
        Debug.Log(GoldGameManagment.goldErnedballsList.Count);
        goldScoreShow.text = goldResultScore.ToString("0");
        goldResultScoreShow.text = goldResultScore.ToString("0");
        if (GoldGameManagment.goldErnedballsList.Count <= 0)
        {
            goldResultsScreen.SetActive(true);
            return;
        }
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("GoldGame");
    }

    public void OnClickMenu()
    {
        if (goldResultScore > GoldLoader.goldBestScoreValue)
            GoldLoader.goldBestScoreValue = goldResultScore;
        SceneManager.LoadScene("GoldMenu");
    }
}
