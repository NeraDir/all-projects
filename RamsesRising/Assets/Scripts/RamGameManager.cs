using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RamGameManager : MonoBehaviour
{
    public static float timeSpawnCrystall;

    public static Sprite jarSprite;

    public static int needIndexCrystall;

    public static float jarHealth;

    public static float crystallDamage;

    public GameObject crystallPrefab;

    public static bool GameEnded;

    public Transform parrentOfSpawnedCrystalls;

    public static float crystallMovementSpeed;

    public static Sprite needCrystallSprite;

    public Image needCrystallImage;

    public static float currentFillValue;

    public Image jarImage;

    public GameObject resultate;

    public GameObject looseResulte;

    private void LateUpdate()
    {
        
        needCrystallImage.sprite = needCrystallSprite;
        jarImage.sprite = jarSprite;
    }

    public void OpenResultpanel() 
    {
        GameEnded = true;
        resultate.SetActive(true);
    }

    public void OpenLooseResultate() 
    {
        GameEnded = true;
        looseResulte.SetActive(true);
    }

    private IEnumerator Start()
    {
        currentFillValue = 0;
        GameEnded = false;
        yield return new WaitForSeconds(1.5f);
        while (!GameEnded) 
        {
            yield return new WaitForSeconds(timeSpawnCrystall);
            if (Random.Range(0, 2) != 0) 
            {
                if (Random.Range(0,2) != 0)
                {
                    GameObject tempCrystall = Instantiate(crystallPrefab, parrentOfSpawnedCrystalls.position, Quaternion.identity);
                    tempCrystall.transform.parent = parrentOfSpawnedCrystalls;
                    tempCrystall.transform.localScale = new Vector3(1, 1, 1);
                    tempCrystall.transform.localPosition = new Vector2(-582f, -1028.75f);
                }
                else
                {
                    GameObject tempCrystall = Instantiate(crystallPrefab, parrentOfSpawnedCrystalls.position, Quaternion.identity);
                    tempCrystall.transform.parent = parrentOfSpawnedCrystalls;
                    tempCrystall.transform.localScale = new Vector3(1,1,1);
                    tempCrystall.transform.localPosition = new Vector2(-582f, 1028.75f);
                }
            }
            else
            {
                if (Random.Range(0, 2) != 0)
                {
                    GameObject tempCrystall = Instantiate(crystallPrefab, parrentOfSpawnedCrystalls.position, Quaternion.identity);
                    tempCrystall.transform.parent = parrentOfSpawnedCrystalls;
                    tempCrystall.transform.localScale = new Vector3(1, 1, 1);
                    tempCrystall.transform.localPosition = new Vector2(582f, -1028.75f);
                }
                else
                {
                    GameObject tempCrystall = Instantiate(crystallPrefab, parrentOfSpawnedCrystalls.position, Quaternion.identity);
                    tempCrystall.transform.parent = parrentOfSpawnedCrystalls;
                    tempCrystall.transform.localScale = new Vector3(1, 1, 1);
                    tempCrystall.transform.localPosition = new Vector2(582f, 1028.75f);
                }
            }
        }
    }

    public void OnClickMenu() 
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OnClickRestart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
