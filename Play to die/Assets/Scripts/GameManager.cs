using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int scoreToDie;

    [SerializeField] public GameObject arrowPrefab;
    [SerializeField] public Transform spawnPos;
    [SerializeField] public TextMeshProUGUI textMessageWin;

    void Start()
    {
        StartGame();
    }

    // Update is called once per frame

    void StartGame()
    {
        scoreToDie = 1000;

        StartCoroutine(SpawnArrow());
    }

    IEnumerator SpawnArrow()
    {

        yield return new WaitForSeconds(1f);

        Instantiate(arrowPrefab, spawnPos.position + new Vector3(Random.Range(-2, 2), 0, 0), arrowPrefab.transform.rotation);

        if (scoreToDie <= 0)
        {
            textMessageWin.gameObject.SetActive(true);

            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        else
        {
            StartCoroutine(SpawnArrow());

        }

    }
}
