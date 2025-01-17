using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallMenuLoader : MonoBehaviour
{
    public GameObject wallballPref;

    public Transform[] wallBallsSpawnPositions;

    private IEnumerator Start()
    {
        StartCoroutine(SpawningBalls());
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("WallsDestroyerMenuScene");
    }

    private IEnumerator SpawningBalls() 
    {
        while (true)
        {
            float rndScale = Random.Range(0.5f, 1f);
            GameObject tempBall = Instantiate(wallballPref, new Vector3(Random.Range(wallBallsSpawnPositions[0].position.x, wallBallsSpawnPositions[1].position.x), wallBallsSpawnPositions[0].position.y, wallBallsSpawnPositions[0].position.z), Quaternion.Euler(0, 0, Random.Range(-360, 360)), wallBallsSpawnPositions[0].parent);
            tempBall.transform.localScale = new Vector3(rndScale, rndScale, rndScale);
            tempBall.transform.SetSiblingIndex(0);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
