using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaryerController : MonoBehaviour
{
    private Rigidbody rb;

    public TMP_Text[] showScore;

    public int score;

    public GameObject[] healthsImages;

    public int healthCount;

    public GameObject badTxt;

    public GameObject goodTxt;

    public GameObject resultPanel;

    private void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        foreach (var item in showScore)
        {
            item.text = score.ToString("0");
        }


        Vector3 mPso = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mDirect = mPso - transform.position;

        rb.velocity = mDirect * 5;

        for (int i = 0; i < healthsImages.Length; i++)
        {
            if (i < healthCount)
            {
                healthsImages[i].SetActive(true);
            }
            else
            {
                healthsImages[i].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MathComponent mather))
        {
            foreach (var item in FindObjectsOfType<MathComponent>())
            {
                Destroy(item.gameObject);
            }
            if (mather.value == MathManager.resultVal)
            {
                goodTxt.SetActive(true);
                score += Random.Range(5, 30);
                if (score> LPlanerDate.BestScore)
                {
                    LPlanerDate.BestScore = score;
                }
                StartCoroutine(DisactivateTxt(goodTxt));
            }
            else
            {
                badTxt.SetActive(true);
                healthCount--;
                if (healthCount <=0)
                {
                    resultPanel.SetActive(true);
                    Time.timeScale = 0;
                }
                StartCoroutine(DisactivateTxt(badTxt));
            }
            FindObjectOfType<MathManager>().SpawnNewMAths();
        }
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SceneMeney");

    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator DisactivateTxt(GameObject txt) 
    {
        yield return new WaitForSeconds(1);
        txt.SetActive(false);
    }
}
