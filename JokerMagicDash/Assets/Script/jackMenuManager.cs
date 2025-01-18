using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jackMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject jackHowToPaly;

    [SerializeField]
    private TMP_Text jackShowScore;

    [SerializeField]
    private GameObject jackDiceObject;

    [SerializeField]
    private Transform[] jackDiceSpawnPlaces;

    [SerializeField]
    private Transform[] jackObjectsForAnimation;

    private List<GameObject> jackSpawnedPool = new List<GameObject>();

    private float timer = 0;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("JackGameShowedHowToPlayString"))
        {
            jackHowToPaly.SetActive(true);
            PlayerPrefs.SetInt("JackGameShowedHowToPlayString", 1);
        }
        StartCoroutine(SpawningDices());
        StartAnimating();
    }

    private void StartAnimating() 
    {
        StartCoroutine(GoLow());
    }

    private IEnumerator GoLow() 
    {
        while (jackObjectsForAnimation[0].localScale.x > 0.7f)
        {
            foreach (var item in jackObjectsForAnimation)
            {
                item.localScale = Vector3.MoveTowards(item.localScale, new Vector3(0.7f, 0.7f, 0.7f), 0.45f * Time.deltaTime);
            }
            yield return null;
        }
        StartCoroutine(GoHigher());
    }

    private IEnumerator GoHigher() 
    {
        while (jackObjectsForAnimation[0].localScale.x < 1)
        {
            foreach (var item in jackObjectsForAnimation)
            {
                item.localScale = Vector3.MoveTowards(item.localScale, Vector3.one, 0.45f * Time.deltaTime);
            }
            yield return null;
        }
        StartCoroutine(GoLow());
    }

    private IEnumerator SpawningDices() 
    {
        float tiemr = 0;
        while (true)
        {
            jackSpawnedPool.Add(Instantiate(jackDiceObject, new Vector3(Random.Range(jackDiceSpawnPlaces[0].position.x, jackDiceSpawnPlaces[1].position.x), jackDiceSpawnPlaces[0].position.y, jackDiceSpawnPlaces[0].position.z), Quaternion.identity));
            tiemr += 0.1f;
            if (tiemr >= 0.2f)
            {
                Destroy(jackSpawnedPool[0].gameObject);
                jackSpawnedPool.Remove(jackSpawnedPool[0]);
                tiemr = 0;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void LateUpdate()
    {
        jackShowScore.text = jackLoaderDiceComponent.BestScore.ToString();
        timer += Time.deltaTime;
        if (timer >= 2)
        {
            jackLoaderDiceComponent.BestScore += 1;
            timer = 0;
        }
    }

    public void OnClickGame() 
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickClose() 
    {
        Application.Quit();
    }
}
