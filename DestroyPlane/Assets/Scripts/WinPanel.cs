using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> activeStars;

    private int activeStartsCount;

    [SerializeField]
    private GameObject targetChecker;

    private void OnEnable()
    {
        PlayerPrefs.SetInt("LasrTargetPlaneColorSave", Game.currentLevelColorIndex);

        Stars.points += Game.health;

        for (int i = 0; i < activeStars.Count; i++)
        {
            activeStars[i].SetActive(false);
        }

        activeStartsCount = Game.health;
        targetChecker.SetActive(false);

    }

    public void StartShowStars()
    {
        StartCoroutine(showStars());
    }

    private IEnumerator showStars()
    {

        Debug.Log(Game.health);


        for (int i = 0; i < Game.health; i++)
        {
            activeStars[i].SetActive(true);
            yield return new WaitForSeconds(0.6f);
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("HomeScene");
    }
    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
