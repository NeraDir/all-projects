using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimGameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] currentTXTShower;

    [SerializeField]
    private GameObject winPage;

    public static List<GameObject> ballsList = new List<GameObject>();

    public List<GameObject> balls = new List<GameObject> ();

    public IEnumerator INIT()
    {
        while (CheckBalls())
        {
            yield return null;
        }
        winPage.SetActive(true);
    }

    private bool CheckBalls() 
    {
        if (ballsList.Count <= 0)
        {
            return false;
        }
        return true;
    }

    private void LateUpdate()
    {
        balls = ballsList;
        foreach (var item in currentTXTShower)
        {
            item.text = "X" + SimSaves.simCurrentScore.ToString("0");
        }
    }

    public void ClickMenu() 
    {
        SceneManager.LoadScene("SimMenu");
    }

    public void ClickRestart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
